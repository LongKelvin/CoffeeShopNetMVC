using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

namespace CoffeeShop.Data.Repositories
{
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Tag GetByIdString(string id)
        {
            return DbContext.Set<Tag>().Find(id);
        }
    }
}