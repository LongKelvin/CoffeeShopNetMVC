using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Data.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Post> GetAllByTag(string tag, out int totalRow, int pageIndex = 0, int pageSize = 50)
        {
            totalRow = 0;

            //use linq staments
            var query = from post in DbContext.Posts
                        where post.Tags.Any(t => t.ID.ToLower().Equals(tag.ToLower()))
                        orderby post.CreatedDate descending
                        select post;

            //Or linq lambda
            //var queryStament = DbContext.Tags.Where(p => p.Name.ToLower().Equals(tag.ToLower())).SelectMany(p => p.Posts);

            totalRow = query.Count();

            query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return query;
        }
    }
}