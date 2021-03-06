using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IProductCategoryRepository _productCategoryRepository { get; set; }

        public ProductCategoryService(IUnitOfWork unitOfWork, IProductCategoryRepository productCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _productCategoryRepository = productCategoryRepository;
        }

        public ProductCategory Add(ProductCategory productCategory)
        {
            return _productCategoryRepository.Add(productCategory);
        }

        public ProductCategory Delete(ProductCategory productCategory)
        {
            return _productCategoryRepository.Delete(productCategory);
        }

        public ProductCategory Delete(int id)
        {
            return _productCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _productCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all Product by tag
            return _productCategoryRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public IEnumerable<ProductCategory> GetAllPaging(string keyWord, int page, int pageSize, out int totalRow)
        {
            if (string.IsNullOrEmpty(keyWord))
                return _productCategoryRepository.GetMultiPaging(null, out totalRow, page, pageSize);

            return _productCategoryRepository.GetMultiPaging(x => x.Name.Contains(keyWord) ||
            x.Alias.Contains(keyWord), out totalRow, page, pageSize);
        }

        public ProductCategory GetById(int id)
        {
            return _productCategoryRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategory productCategory)
        {
            _productCategoryRepository.Update(productCategory);
        }

        public IEnumerable<ProductCategory> GetAll(string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
                return GetAll();

            return _productCategoryRepository.GetMulti(x => x.Name.Contains(keyWord) || x.Alias.Contains(keyWord));
        }

        public void DeleteMultiItems(int[] ids)
        {
            _productCategoryRepository.DeleteMulti(x => ids.Contains(x.ID));
        }
    }
}