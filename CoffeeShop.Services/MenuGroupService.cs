using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public class MenuGroupService : IMenuGroupService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IMenuGroupRepository _menuGroupRepository { get; set; }

        public MenuGroupService(IUnitOfWork unitOfWork, IMenuGroupRepository menuGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _menuGroupRepository = menuGroupRepository;
        }

        public void Add(MenuGroup MenuGroup)
        {
            _menuGroupRepository.Add(MenuGroup);
        }

        public void Delete(MenuGroup MenuGroup)
        {
            _menuGroupRepository.Delete(MenuGroup);
        }

        public void Delete(int id)
        {
            _menuGroupRepository.Delete(id);
        }

        public IEnumerable<MenuGroup> GetAll()
        {
            return _menuGroupRepository.GetAll();
        }

        public IEnumerable<MenuGroup> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all MenuGroup by tag
            return _menuGroupRepository.GetMultiPaging(null, out totalRow, page, pageSize);
        }

        public IEnumerable<MenuGroup> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _menuGroupRepository.GetMultiPaging(null, out totalRow, page, pageSize);
        }

        public MenuGroup GetById(int id)
        {
            return _menuGroupRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(MenuGroup MenuGroup)
        {
            _menuGroupRepository.Update(MenuGroup);
        }
    }
}