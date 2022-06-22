using AutoMapper;

using CoffeeShop.Common;
using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;
using CoffeeShop.Web.Infrastucture.Extensions;
using CoffeeShop.Web.Models;

using Newtonsoft.Json;

using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace CoffeeShop.Web.Api
{
    [RoutePrefix(CommonConstants.API_Product)]
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService, IErrorService errorService) : base(errorService)
        {
            _productService = productService;
        }

        // GET api/<controller>
        [Authorize]
        [Route("GetAll")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                //Init totalRow
                int totalRow = 0;
                //Get All Product

                var listProduct = _productService.GetAll(keyWord);

                totalRow = listProduct.Count();

                //Order by
                IEnumerable<Product> query = listProduct.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                //Map object using Automapper
                var listProductVM = Mapper.Map<List<ProductViewModel>>(query);

                //Paging
                var paginationSetResult = new PaginationSet<ProductViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    Items = listProductVM,
                    //Rounding decimals
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                return request.CreateResponse(HttpStatusCode.OK, paginationSetResult);
            });
        }

        // GET api/<controller>
        [Route("GetById/{id:int}")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int Id)
        {
            return CreateHttpResponse(request, () =>
            {
                var ProductDetail = _productService.GetByCondition(x => x.ID == Id, new string[] { "Tags" });

                if (ProductDetail == null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, Id.ToString());
                }

                //Map object using Automapper
                var productCategotyVM = Mapper.Map<ProductViewModel>(ProductDetail);

                var listMoreImages = productCategotyVM.MoreImages.Split(',').ToList();
                try
                {
                    var jsonMoreImages = JsonConvert.SerializeObject(listMoreImages);
                    productCategotyVM.MoreImages = jsonMoreImages;
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    productCategotyVM.MoreImages = string.Empty;
                }

                productCategotyVM.TagsString = EntityExtensions.GetTagStringFromProductTags(ProductDetail);

                return request.CreateResponse(HttpStatusCode.OK, productCategotyVM);
            });
        }

        [Route("Create")]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel ProductVM)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                var newProduct = new Product();
                EntityExtensions.UpdateProduct(newProduct, ProductVM);
                newProduct.CreatedBy = User.Identity.Name;

                try
                {
                    var jsonMoreImages = JsonConvert.DeserializeObject<List<string>>(newProduct.MoreImages);
                    newProduct.MoreImages = string.Join(",", jsonMoreImages);
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    newProduct.MoreImages = null;
                }

                var result = _productService.Add(newProduct);
                _productService.SaveChanges();

                var responseResult = Mapper.Map<Product, ProductViewModel>(result);
                responseResult.TagsString = EntityExtensions.GetTagStringFromProductTags(result);
                return request.CreateResponse(HttpStatusCode.Created, responseResult);
            });
        }

        [Route("GetAllCategories")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetAll();
                var responseData = Mapper.Map<List<Product>, List<ProductViewModel>>(model.ToList());
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("Update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel ProductVM)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                var updateProduct = new Product();
                EntityExtensions.UpdateProduct(updateProduct, ProductVM);
                updateProduct.UpdatedBy = User.Identity.Name;

                try
                {
                    var jsonMoreImages = JsonConvert.DeserializeObject<List<string>>(updateProduct.MoreImages);
                    updateProduct.MoreImages = string.Join(",", jsonMoreImages);
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    updateProduct.MoreImages = null;
                }

                var result = _productService.Update(updateProduct);
                _productService.SaveChanges();

                var responseResult = Mapper.Map<Product, ProductViewModel>(result);

                return request.CreateResponse(HttpStatusCode.OK, responseResult);
            });
        }

        [Route("Delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                _productService.Delete(id);
                _productService.SaveChanges();

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [Route("DeleteMultiItems")]
        [HttpDelete]
        public HttpResponseMessage DeleteMultiItems(HttpRequestMessage request, string ids)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                try
                {
                    var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(ids);
                    foreach (var item in listProduct)
                    {
                        _productService.Delete(item);
                    }

                    _productService.SaveChanges();
                }
                catch
                {
                    throw;
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [Route("ImportFromExcel")]
        [HttpPost]
        public async Task<HttpResponseMessage> ImportFromExcel()
        {
            if (!Request.Content.IsMimeMultipartContent())
                return Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Định dạng không được hỗ trợ");

            var rootFolderPath = HttpContext.Current.Server.MapPath(ConfigHelper.GetByKey(CommonConstants.EXCEL_UPLOAD_PATH));
            if (!Directory.Exists(rootFolderPath))
                Directory.CreateDirectory(rootFolderPath);

            var provider = new MultipartFormDataStreamProvider(rootFolderPath);
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            // Show all the key-value pairs.
            // Just for debug
            //foreach (var key in result.FormData.AllKeys)
            //{
            //    foreach (var val in result.FormData.GetValues(key))
            //    {
            //        Trace.WriteLine(string.Format("{0}: {1}", key, val));
            //    }
            //}

            //foreach (MultipartFileData file in result.FileData)
            //{
            //    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
            //    Trace.WriteLine("Server file path: " + file.LocalFileName);
            //}

            if (result.FormData["CategoryId"] == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bạn chưa chọn danh mục sản phẩm");

            //Upload files
            int addedCount = 0;
            int categoryId = 0;

            int.TryParse(result.FormData["CategoryId"], out categoryId);

            var categoryById = _productService.GetCategory(categoryId);
            if (categoryById == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Danh mục sản phâm không đúng");

            if (result.FileData.Count <= 0)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "File rỗng, không có dữ liệu để nhập vào hệ thống");

            try
            {
                foreach (var fileData in result.FileData)
                {
                    if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "File không đúng định dạng");

                    string fileName = fileData.Headers.ContentDisposition.FileName;
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        fileName = fileName.Trim('"');

                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        fileName = Path.GetFileName(fileName);

                    var fullPath = Path.Combine(rootFolderPath, fileName);
                    File.Copy(fileData.LocalFileName, fullPath, true);

                    //Insert to database
                    var listProduct = this.ReadProductFromExcel(fullPath, categoryId);
                    if (listProduct.Count > 0)
                    {
                        foreach (var product in listProduct)
                        {
                            _productService.Add(product);
                            addedCount++;
                        }

                        _productService.SaveChanges();
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, $"Đã nhập thành công {addedCount} sản phẩm");
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Đã có lỗi xảy ra, Vui lòng kiểm tra lại file Excel để format theo đúng mẫu Excel từ hệ thống");
            }
        }

        private List<Product> ReadProductFromExcel(string fullPath, int categoryId)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault();

                //Delete empty row from Excel
                workSheet.TrimLastEmptyRows();

                List<Product> listProducts = new List<Product>();
                ProductViewModel productViewModel;
                Product product;

                decimal originalPrice = 0;
                decimal price = 0;
                decimal promotionPrice;
                int quantity = 0;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    try
                    {
                        productViewModel = new ProductViewModel
                        {
                            Name = workSheet.Cells[i, 1].Value.ToString(),
                            Alias = StringHelper.ToUnsignString(workSheet.Cells[i, 1].Value.ToString()),
                            Description = workSheet.Cells[i, 2].Value.ToString(),

                            //This block throw exception about casting data type
                            //OriginalPrice = (decimal)workSheet.Cells[i, 3].Value,
                            //Price = (decimal)workSheet.Cells[i, 4].Value,
                            //PromotionPrice = (decimal?)workSheet.Cells[i, 5].Value,
                            //Quantity = (int)workSheet.Cells[i, 6].Value,

                            Status = ExcelHelper.ExtractTrueFalseValue(workSheet.Cells[i, 7].Value),
                            HomeFlag = ExcelHelper.ExtractTrueFalseValue(workSheet.Cells[i, 8].Value),
                            HotFlag = ExcelHelper.ExtractTrueFalseValue(workSheet.Cells[i, 9].Value),
                            CategoryID = categoryId,
                        };

                        int.TryParse(workSheet.Cells[i, 6].Value.ToString().Replace(",", ""), out quantity);
                        productViewModel.Quantity = quantity;

                        decimal.TryParse(workSheet.Cells[i, 3].Value.ToString().Replace(",", ""), out originalPrice);
                        productViewModel.OriginalPrice = originalPrice;

                        decimal.TryParse(workSheet.Cells[i, 4].Value.ToString().Replace(",", ""), out price);
                        productViewModel.Price = price;

                        if (decimal.TryParse(workSheet.Cells[i, 5].Value.ToString(), out promotionPrice))
                        {
                            productViewModel.PromotionPrice = promotionPrice;
                        }

                        product = new Product();
                        product.UpdateProduct(productViewModel);
                        listProducts.Add(product);
                    }
                    catch (Exception eex)
                    {
                        LogError(eex);
                        return listProducts;
                    }
                }
                return listProducts;
            }
        }

        [HttpGet]
        [Route("ExportToExcel")]
        public async Task<HttpResponseMessage> ExportToExcel(HttpRequestMessage request, string keyWord = null)
        {
            string fileName = string.Concat("Product_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey(CommonConstants.EXCEL_EXPORT_PATH);
            string filePath = HttpContext.Current.Server.MapPath(folderReport);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            string fullPath = Path.Combine(filePath, fileName);

            try
            {
                var data = _productService.GetAll(keyWord).ToList();
                await ReportHelper.GenerateExcelXls(data, fullPath);
                Trace.WriteLine($"PATH DOWNLOAD URL: {Path.Combine(folderReport, fileName)}");
                return request.CreateResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName));
            }
            catch (Exception ex)
            {
                LogError(ex);
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("ExportToPdf")]
        public async Task<HttpResponseMessage> ExportToPdf(HttpRequestMessage request, string keyWord)
        {
            string fileName = string.Concat("Product_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".pdf");
            var folderReport = ConfigHelper.GetByKey(CommonConstants.PDF_EXPORT_PATH);
            string filePath = HttpContext.Current.Server.MapPath(folderReport);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            string fullPath = Path.Combine(filePath, fileName);

            try
            {
                //var data = _productService.GetAll(keyWord, new string[] { "ProductCategory" }).ToList();
                var data = _productService.GetAll(keyWord).ToList();

                //string htmlTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("/Assets/Admin/templates/product-report-template.html"));
                string razorViewTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("/Views/Shared/Templates/ProductListTemplate.cshtml"));
                razorViewTemplate = razorViewTemplate.Replace("{{CreatedDate}}", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
                var htmlParseViewData = RazorEngine.Razor.Parse(razorViewTemplate, data);

                await ReportHelper.GeneratePdf(htmlParseViewData, fullPath);
                Trace.WriteLine($"PATH DOWNLOAD URL: {Path.Combine(folderReport, fileName)}");
                return request.CreateResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName));
            }
            catch (Exception ex)
            {
                LogError(ex);
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}