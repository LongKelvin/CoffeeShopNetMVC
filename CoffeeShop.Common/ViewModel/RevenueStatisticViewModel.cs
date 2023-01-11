using System;
using System.Globalization;

namespace CoffeeShop.Common.ViewModel
{
    public class RevenueStatisticViewModel
    {
        public string Date { get; set; }

        public decimal Revenues { get; set; }

        public decimal Benefit { get; set; }
    }
}