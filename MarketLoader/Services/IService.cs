using System.Collections.Generic;
using MarketLoader.Entities;

namespace MarketLoader.Services
{
    interface IService
    {
        IEnumerable<Quote> GetWeeklyData(string symbol);

        IEnumerable<Market> GetWeeklyDataForAllSymbols();
    }
}
