using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Data.Repositories
{
    public interface IOrderInvoiceRepository : IRepository<OrderInvoice>
    {

    }
    public class OrderInvoiceRepository : RepositoryBase<OrderInvoice>, IOrderInvoiceRepository
    {
        public OrderInvoiceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
