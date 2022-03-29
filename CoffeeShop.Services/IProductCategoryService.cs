using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory productCategory);

        void Update(ProductCategory productCategory);

        ProductCategory Delete(ProductCategory productCategory);

        ProductCategory Delete(int id);

        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAll(string keyWord);

        IEnumerable<ProductCategory> GetAllPaging(string keyWord,int page, int pageSize, out int totalRow);

        IEnumerable<ProductCategory> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        ProductCategory GetById(int id);

        void SaveChanges();
    }
}