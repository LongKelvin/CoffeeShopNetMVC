using CoffeeShop.Common.ViewModel;
using CoffeeShop.Data.Insfrastructure;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

using static log4net.Appender.RollingFileAppender;

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
            var frDate = DateTime.Parse(fromDate).ToString("yyyy/MM/dd");
            var tDate = DateTime.Parse(toDate).ToString("yyyy/MM/dd"); ;
            var parameters = new SqlParameter[]{
                new SqlParameter() {
                    ParameterName = "@fromDate",
                    Value = frDate
            },
                new SqlParameter()
                {
                    ParameterName = "@toDate",
                    Value = tDate
                }
            };

            return DbContext.Database
                .SqlQuery<RevenueStatisticViewModel>("GetRevenueStatistic @fromDate, @toDate", parameters).ToList();
        }
    }
}