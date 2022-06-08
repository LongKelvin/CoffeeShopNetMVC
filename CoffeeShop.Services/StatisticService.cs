using CoffeeShop.Common.ViewModel;
using CoffeeShop.Data.Repositories;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public interface IStatisticService
    {
        List<RevenueStatisticViewModel> GetRevenueStatistics(string fromDate, string toDate);
    }

    public class StatisticService : IStatisticService
    {
        private readonly IStatisticRepository _statisticRepository;

        public StatisticService(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }

        public List<RevenueStatisticViewModel> GetRevenueStatistics(string fromDate, string toDate)
        {
            return _statisticRepository.GetRevenueStatistic(fromDate, toDate);
        }
    }
}