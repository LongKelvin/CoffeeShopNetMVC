using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        void SaveChanges();
    }
}
