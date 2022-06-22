using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public interface ITagService
    {
        void Add(Tag tag);

        void Update(Tag tag);

        void Delete(Tag tag);

        void Delete(int id);

        IEnumerable<Tag> GetAll();

        IEnumerable<Tag> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Tag> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        Tag GetById(int id);

        Tag GetByIdString(string id);

        void SaveChanges();
    }
}