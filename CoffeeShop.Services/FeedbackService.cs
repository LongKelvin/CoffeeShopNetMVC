using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackService(IFeedbackRepository feedbackRepository,
            IUnitOfWork unitOfWork)
        {
            _feedbackRepository = feedbackRepository;
            _unitOfWork = unitOfWork;
        }

        public Feedback Add(Feedback feedback)
        {
            return _feedbackRepository.Add(feedback);
        }

        public Feedback Delete(Feedback feedback)
        {
            return _feedbackRepository.Delete(feedback);
        }

        public Feedback Delete(int id)
        {
            return _feedbackRepository.Delete(id);
        }

        public void DeleteMultiItems(int[] ids)
        {
            _feedbackRepository.DeleteMulti(x => ids.Contains(x.ID));
        }

        public IEnumerable<Feedback> GetAll()
        {
            return _feedbackRepository.GetAll();
        }

        public IEnumerable<Feedback> GetAll(string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
                return GetAll();

            return _feedbackRepository.GetMulti(x => x.Name.Contains(keyWord) || x.Message.Contains(keyWord));
        }

        public Feedback GetById(int id)
        {
            return _feedbackRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Feedback feedback)
        {
            _feedbackRepository.Update(feedback);
        }
    }
}