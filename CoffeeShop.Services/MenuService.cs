using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public class MenuService : IMenuService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IMenuRepository _menuRepository { get; set; }

        public MenuService(IUnitOfWork unitOfWork, IMenuRepository menuRepository)
        {
            _unitOfWork = unitOfWork;
            _menuRepository = menuRepository;
        }

        public void Add(Menu menu)
        {
            _menuRepository.Add(menu);
        }

        public void Delete(Menu menu)
        {
            _menuRepository.Delete(menu);
        }

        public void Delete(int id)
        {
            _menuRepository.Delete(id);
        }

        public IEnumerable<Menu> GetAll()
        {
            return _menuRepository.GetAll();
        }

        public IEnumerable<Menu> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all Menu by tag
            return _menuRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public IEnumerable<Menu> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _menuRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public Menu GetById(int id)
        {
            return _menuRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Menu menu)
        {
            _menuRepository.Update(menu);
        }
    }
}