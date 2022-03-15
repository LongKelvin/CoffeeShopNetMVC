using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public class SlideService : ISlideService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public ISlideRepository _slideRepository { get; set; }

        public SlideService(IUnitOfWork unitOfWork, ISlideRepository slideRepository)
        {
            _unitOfWork = unitOfWork;
            _slideRepository = slideRepository;
        }

        public void Add(Slide slide)
        {
            _slideRepository.Add(slide);
        }

        public void Delete(Slide slide)
        {
            _slideRepository.Delete(slide);
        }

        public void Delete(int id)
        {
            _slideRepository.Delete(id);
        }

        public IEnumerable<Slide> GetAll()
        {
            return _slideRepository.GetAll();
        }

        public IEnumerable<Slide> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all Slide by tag
            return _slideRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public IEnumerable<Slide> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _slideRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public Slide GetById(int id)
        {
            return _slideRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide slide)
        {
            _slideRepository.Update(slide);
        }
    }
}