using System.Collections.Generic;
using MarketLoader.Entities;

namespace MarketLoader.Services
{
    delegate void Downloading(object sender, string code);

    interface IService
    {
        IEnumerable<Quote> GetWeeklyData(string symbol);

        IEnumerable<Market> GetWeeklyDataForAllSymbols();
    
        event Downloading Download;
    }
}
