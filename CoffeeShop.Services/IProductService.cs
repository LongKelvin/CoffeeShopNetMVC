﻿using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoffeeShop.Services
{
    public interface IProductService
    {
        Product Add(Product product);

        Product Update(Product product);

        void Delete(Product product);

        void Delete(int id);

        IEnumerable<Product> GetAll(string keyWord);
        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        Product GetById(int id);
        List<Product> GetListProductByParentID(int id);
        List<Product> GetListProductByParentID(int id, int page, int pageSize, string sort , out int totalRow);
        Product GetByCondition(Expression<Func<Product, bool>> expression, string[] includes = null);
        List<Product> GetListProductByCondition(Expression<Func<Product, bool>> expression, string[] includes = null);
        List<Product> GetListProductByConditionPaging(Expression<Func<Product, bool>> expression, int page, int pageSize, string sort, out int totalRow, string[] includes = null);

        void SaveChanges();
    }
}