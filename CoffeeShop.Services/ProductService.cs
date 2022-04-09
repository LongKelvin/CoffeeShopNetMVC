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

        public ProductService(IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            ITagRepository tagRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _tagRepository = tagRepository;
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

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll(new string[] { "ProductCategory" });
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

            productFromDb.ID = product.ID;
            productFromDb.Name = product.Name;
            productFromDb.Description = product.Description;
            productFromDb.Alias = product.Alias;
            productFromDb.CategoryID = product.CategoryID;
            productFromDb.Content = product.Content;
            productFromDb.Images = product.Images;
            productFromDb.MoreImages = product.MoreImages;
            productFromDb.Price = product.Price;
            productFromDb.PromotionPrice = product.PromotionPrice;
            productFromDb.Warranty = product.Warranty;
            productFromDb.HomeFlag = product.HomeFlag;
            productFromDb.ViewCount = product.ViewCount;
            productFromDb.HotFlag = product.HotFlag;
            productFromDb.ManufacturingDate = product.ManufacturingDate;
            productFromDb.ExpireDate = product.ExpireDate;
            productFromDb.CreatedDate = product.CreatedDate;
            productFromDb.CreatedBy = product.CreatedBy;
            productFromDb.UpdatedDate = product.UpdatedDate;
            productFromDb.UpdatedBy = product.UpdatedBy;
            productFromDb.MetaKeyword = product.MetaKeyword;
            productFromDb.MetaDescription = product.MetaDescription;
            productFromDb.Status = product.Status;
            productFromDb.RowVersion = product.RowVersion;

            productFromDb.Tags.Clear();

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

        private CoffeeShopDbContext DbContext
        {
            get { return _productRepository.DbContext; }
        }
    }
}