using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public class SupportOnlineService : ISupportOnlineService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public ISupportOnlineRepository _supportOnlineRepository { get; set; }

        public SupportOnlineService(IUnitOfWork unitOfWork, ISupportOnlineRepository supportOnlineRepository)
        {
            _unitOfWork = unitOfWork;
            _supportOnlineRepository = supportOnlineRepository;
        }

        public void Add(SupportOnline supportOnline)
        {
            _supportOnlineRepository.Add(supportOnline);
        }

        public void Delete(SupportOnline supportOnline)
        {
            _supportOnlineRepository.Delete(supportOnline);
        }

        public void Delete(int id)
        {
            _supportOnlineRepository.Delete(id);
        }

        public IEnumerable<SupportOnline> GetAll()
        {
            return _supportOnlineRepository.GetAll();
        }

        public IEnumerable<SupportOnline> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all SupportOnline by tag
            return _supportOnlineRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public IEnumerable<SupportOnline> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _supportOnlineRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public SupportOnline GetById(int id)
        {
            return _supportOnlineRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(SupportOnline supportOnline)
        {
            _supportOnlineRepository.Update(supportOnline);
        }
    }
}