using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public class PageService : IPageService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IPageRepository _pageRepository { get; set; }

        public PageService(IUnitOfWork unitOfWork, IPageRepository pageRepository)
        {
            _unitOfWork = unitOfWork;
            _pageRepository = pageRepository;
        }

        public void Add(Page page)
        {
            _pageRepository.Add(page);
        }

        public void Delete(Page page)
        {
            _pageRepository.Delete(page);
        }

        public void Delete(int id)
        {
            _pageRepository.Delete(id);
        }

        public IEnumerable<Page> GetAll()
        {
            return _pageRepository.GetAll();
        }

        public IEnumerable<Page> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all Page by tag
            return _pageRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public IEnumerable<Page> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _pageRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public Page GetById(int id)
        {
            return _pageRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Page page)
        {
            _pageRepository.Update(page);
        }
    }
}