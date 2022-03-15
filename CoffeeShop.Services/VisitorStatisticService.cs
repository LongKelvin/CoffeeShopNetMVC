using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public class VisitorStatisticService : IVisitorStatisticService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IVisitorStatisticRepository _visitorStatisticRepository { get; set; }

        public VisitorStatisticService(IUnitOfWork unitOfWork, IVisitorStatisticRepository visitorStatisticRepository)
        {
            _unitOfWork = unitOfWork;
            _visitorStatisticRepository = visitorStatisticRepository;
        }

        public void Add(VisitorStatistic visitorStatistic)
        {
            _visitorStatisticRepository.Add(visitorStatistic);
        }

        public void Delete(VisitorStatistic visitorStatistic)
        {
            _visitorStatisticRepository.Delete(visitorStatistic);
        }

        public void Delete(int id)
        {
            _visitorStatisticRepository.Delete(id);
        }

        public IEnumerable<VisitorStatistic> GetAll()
        {
            return _visitorStatisticRepository.GetAll();
        }

        public IEnumerable<VisitorStatistic> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all VisitorStatistic by tag
            return _visitorStatisticRepository.GetMultiPaging(null, out totalRow, page, pageSize);
        }

        public IEnumerable<VisitorStatistic> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _visitorStatisticRepository.GetMultiPaging(null, out totalRow, page, pageSize);
        }

        public VisitorStatistic GetById(int id)
        {
            return _visitorStatisticRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(VisitorStatistic visitorStatistic)
        {
            _visitorStatisticRepository.Update(visitorStatistic);
        }
    }
}