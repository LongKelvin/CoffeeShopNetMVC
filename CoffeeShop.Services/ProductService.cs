using CoffeeShop.Data;
using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;

using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CoffeeShop.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IProductRepository _productRepository { get; set; }
        public ITagRepository _tagRepository { get; set; }
        public IProductCategoryRepository _productCategoryRepository { get; set; }

        public ProductService(IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            ITagRepository tagRepository,
            IProductCategoryRepository productCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _tagRepository = tagRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        public Product Add(Product product)
        {
            var listTag = new List<Tag>();
            foreach (var tag in product.Tags)
            {
                //check if tag is existing in database
                var existingTag = _tagRepository.GetByIdString(tag.ID);
                if (existingTag == null)
                {
                    Tag t = new Tag
                    {
                        ID = tag.ID,
                        Name = tag.Name,
                        Type = tag.Type,
                    };
                    listTag.Add(t);
                }
                else
                {
                    listTag.Add(existingTag);
                }
            }

            product.Tags = listTag;

            return _productRepository.Add(product);
        }

        public void Delete(Product product)
        {
            _productRepository.Delete(product);
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll(string[] includes = null)
        {
            return _productRepository.GetAll(includes);
            //var res =  _productRepository.GetAll(null);
        }

        public IEnumerable<Product> GetAll(string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
                return GetAll();

            return _productRepository.GetMulti(x => x.Name.Contains(keyWord) || x.Alias.Contains(keyWord));
        }

        public IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all Product by tag
            return _productRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _productRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public Product Update(Product product)
        {
            var productFromDb = DbContext.Products
                .Include(t => t.Tags)
                .FirstOrDefault(p => p.ID == product.ID);

            if (productFromDb == null)
                return null;

            ModelsEntityExtensions.UpdateProduct(product, productFromDb);

            //clear all tags record in link table ProductTags for add new record
            productFromDb.Tags.Clear();

            //Get all tags from product and assign to productFromDb
            var listTag = new List<Tag>();
            foreach (var tag in product.Tags)
            {
                //check if tag is existing in database
                var existingTag = _tagRepository.GetByIdString(tag.ID);
                if (existingTag == null)
                {
                    Tag t = new Tag
                    {
                        ID = tag.ID,
                        Name = tag.Name,
                        Type = tag.Type,
                    };

                    listTag.Add(t);
                }
                else
                {
                    listTag.Add(existingTag);
                }
            }

            productFromDb.Tags = listTag;
            _productRepository.MakeAsModified(productFromDb);

            _unitOfWork.Commit();
            return productFromDb;
        }

        public Product GetByCondition(Expression<Func<Product, bool>> expression, string[] includes = null)
        {
            return _productRepository.GetByCondition(expression, includes);
        }

        public List<Product> GetListProductByParentID(int id)
        {
            return _productRepository.GetMulti(x => x.CategoryID == id).ToList();
        }

        public List<Product> GetListProductByParentID(int id, int page, int pageSize, string sort, out int totalRow)
        {
            IEnumerable<Product> query = new List<Product>();
            if (id == 0)
            {
                query = _productRepository.GetMulti(x => x.Status == true);
            }
            else
            {
                query = _productRepository.GetMulti(x => x.CategoryID == id && x.Status == true);
            }

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;

                case "priceLowHigh":
                    query = query.OrderBy(x => x.Price);
                    break;

                case "priceHighLow":
                    query = query.OrderByDescending(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> GetListProductByCondition(Expression<Func<Product, bool>> expression, string[] includes = null)
        {
            return _productRepository.GetMulti(expression, includes).ToList();
        }

        public List<Product> GetListProductByConditionPaging(Expression<Func<Product, bool>> expression, int page, int pageSize,
            string sort, out int totalRow, string[] includes = null)
        {
            //var query = _productRepository.GetMulti(expression);
            var query = DbContext.Products.Where(expression);

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;

                case "priceLowHigh":
                    query = query.OrderBy(x => x.Price);
                    break;

                case "priceHighLow":
                    query = query.OrderByDescending(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> GetRelatedProduct(int? categoryID)
        {
            List<Product> listRelatedProduct = new List<Product>();
            if (categoryID == null)
                listRelatedProduct = _productRepository.GetMulti(x => x.Status == true)
                    .OrderBy(x => x.CreatedDate).Take(5).ToList();

            listRelatedProduct = _productRepository.GetMulti(x => x.Status == true && x.CategoryID == categoryID)
                .OrderBy(x => x.CreatedDate).Take(5).ToList();
            return listRelatedProduct;
        }

        public List<Product> GetListProductByTag(string tag, int page, int pageSize, string sort, out int totalRow)
        {
            IEnumerable<Product> query = new List<Product>();
            if (string.IsNullOrEmpty(tag))
            {
                query = _productRepository.GetMulti(x => x.Status == true);
            }
            else
            {
                var tagByName = _tagRepository.GetByCondition(x => x.ID.Contains(tag), new string[] { "Products" });
                query = tagByName.Products;
            }

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;

                case "priceLowHigh":
                    query = query.OrderBy(x => x.Price);
                    break;

                case "priceHighLow":
                    query = query.OrderByDescending(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> GetListProductByTag(string tag)
        {
            IEnumerable<Product> query = new List<Product>();
            if (string.IsNullOrEmpty(tag))
            {
                query = _productRepository.GetMulti(x => x.Status == true);
            }
            else
            {
                var tagByName = _tagRepository.GetByCondition(x => x.ID.Contains(tag), new string[] { "Products" });
                query = tagByName.Products;
            }
            return query.ToList();
        }

        public List<Tag> GetTagsByProduct(int productID)
        {
            var product = _productRepository.GetByCondition(x => x.ID == productID,
                new string[] { "Tags" });

            return product.Tags.Select(t => new Tag
            {
                ID = t.ID,
                Name = t.Name,
                Type = t.Type,
            }).ToList();
        }

        public void IncreaseView(int productID)
        {
            var product = _productRepository.GetById(productID);
            if (product.ViewCount.HasValue)
            {
                product.ViewCount += 1;
            }
            else
            {
                product.ViewCount = 1;
            }
        }

        public ProductCategory GetCategory(int productID)
        {
            var product = _productRepository.GetById(productID);
            return _productCategoryRepository.GetByCondition(
                x => x.ID == product.CategoryID && x.Status == true);
        }

        public bool SellProduct(int productId, int quantity)
        {
            var product = _productRepository.GetById(productId);

            if (product.Quantity < quantity)
                return false;

            product.Quantity -= quantity;
            return true;
        }

        private CoffeeShopDbContext DbContext
        {
            get { return _productRepository.DbContext; }
        }
    }
}