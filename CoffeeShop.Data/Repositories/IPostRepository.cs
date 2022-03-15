using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System.Collections;
using System.Collections.Generic;

namespace CoffeeShop.Data.Repositories
{
    public interface IPostRepository: IRepository<Post>
    {
        IEnumerable<Post> GetAllByTag(string tag, out int totalRow, int pageIndex = 0, int pageSize = 50);
    }
}