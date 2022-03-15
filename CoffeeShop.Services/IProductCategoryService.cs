using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public interface IProductCategoryService
    {
        void Add(ProductCategory productCategory);
        void Update(ProductCategory productCategory);
        void Delete(ProductCategory productCategory);
        void Delete(int id);
        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAllPaging(int page, int pageSize, out int totalRow);
        IEnumerable<ProductCategory> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);
        ProductCategory GetById(int id);
        void SaveChanges();
    }
}
