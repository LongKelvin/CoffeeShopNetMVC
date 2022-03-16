using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public class FooterService : IFooterService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IFooterRepository _footerRepository { get; set; }

        public FooterService(IUnitOfWork unitOfWork, IFooterRepository footerRepository)
        {
            _unitOfWork = unitOfWork;
            _footerRepository = footerRepository;
        }

        public void Add(Footer footer)
        {
            _footerRepository.Add(footer);
        }

        public void Delete(Footer footer)
        {
            _footerRepository.Delete(footer);
        }

        public void Delete(int id)
        {
            _footerRepository.Delete(id);
        }

        public IEnumerable<Footer> GetAll()
        {
            return _footerRepository.GetAll(new string[] { "FooterCategory" });
        }

        public IEnumerable<Footer> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all Footer by tag
            return _footerRepository.GetMultiPaging(null, out totalRow, page, pageSize);
        }

        public IEnumerable<Footer> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _footerRepository.GetMultiPaging(null, out totalRow, page, pageSize);
        }

        public Footer GetById(int id)
        {
            return _footerRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Footer footer)
        {
            _footerRepository.Update(footer);
        }
    }
}