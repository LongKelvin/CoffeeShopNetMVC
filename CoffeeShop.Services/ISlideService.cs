using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public interface ISlideService
    {
        Slide Add(Slide slide);

        Slide Update(Slide slide);

        void Delete(Slide slide);

        void Delete(int id);

        IEnumerable<Slide> GetAll();

        IEnumerable<Slide> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Slide> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        Slide GetById(int id);

        void SaveChanges();
    }
}