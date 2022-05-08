using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public interface IFeedbackService
    {
        Feedback Add(Feedback feedback);

        void Update(Feedback feedback);

        Feedback Delete(Feedback feedback);

        Feedback Delete(int id);

        IEnumerable<Feedback> GetAll();

        IEnumerable<Feedback> GetAll(string keyWord);

        Feedback GetById(int id);

        void SaveChanges();

        void DeleteMultiItems(int[] ids);
    }
}