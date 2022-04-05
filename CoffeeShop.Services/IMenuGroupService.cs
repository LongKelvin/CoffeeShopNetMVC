using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public interface IMenuGroupService
    {
        void Add(MenuGroup menuGroup);

        void Update(MenuGroup menuGroup);

        void Delete(MenuGroup menuGroup);

        void Delete(int id);

        IEnumerable<MenuGroup> GetAll();

        IEnumerable<MenuGroup> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<MenuGroup> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        MenuGroup GetById(int id);

        void SaveChanges();
    }
}