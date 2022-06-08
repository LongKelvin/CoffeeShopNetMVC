using CoffeeShop.Common.ViewModel;
using CoffeeShop.Data.Insfrastructure;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CoffeeShop.Data.Repositories
{
    public interface IStatisticRepository : IRepository<RevenueStatisticViewModel>
    {
        List<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);
    }

    public class StatisticRepository : RepositoryBase<RevenueStatisticViewModel>, IStatisticRepository
    {
        public StatisticRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public List<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter() {
                    ParameterName = "@fromDate",
                    Value = fromDate,
                },
                new SqlParameter()
                {
                    ParameterName = "@toDate",
                    Value = toDate,
                }
            };

            return DbContext.Database
                .SqlQuery<RevenueStatisticViewModel>("GetRevenueStatistic @fromDate, @toDate",parameters).ToList();
        }
    }
}