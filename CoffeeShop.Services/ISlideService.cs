using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public interface ISlideService
    {
        void Add(Slide slide);

        void Update(Slide slide);

        void Delete(Slide slide);

        void Delete(int id);

        IEnumerable<Slide> GetAll();

        IEnumerable<Slide> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Slide> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        Slide GetById(int id);

        void SaveChanges();
    }
}