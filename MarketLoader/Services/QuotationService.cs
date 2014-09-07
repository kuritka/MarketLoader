using System;
using System.Collections.Generic;
using System.Linq;
using MarketLoader.Entities;
using MarketLoader.Infrastructure;
using MarketLoader.WebRobots.Barchart.com;
namespace MarketLoader.Services
{
    internal class QuotationService : IService
    {
        internal event Downloading Download;

        internal delegate void Downloading(object sender, string code);
        
        /// <summary>
        /// Gets the historical data for specified symbol in format like KC, CC etc;        
        /// </summary>
        public IEnumerable<Quote> GetWeeklyData(string symbol)
        {            
            var quotes = new HashSet<Quote>();
            var contractSpecification = ContractSpecification.Data.First(d => d.Symbol == symbol);
            var yearlyCodes = GetExpirationMonths(contractSpecification);
            foreach (var code in yearlyCodes)
            {
                OnDownload(code);
                quotes.AddRange(BarChartProxy.GetHistoricalData(code, Period.Weekly, BarChartProxy.FrameSize.Largest));                    
            }            
            return new List<Quote>(quotes).OrderBy(d => d.DateTime);            
        }

        

        
        public IEnumerable<Market> GetWeeklyDataForAllSymbols()
        {
            return ContractSpecification.Data.Select(d => new Market(d, GetWeeklyData(d.Symbol)));
        }

        
        private IEnumerable<string> GetExpirationMonths(ContractSpecification contractSpecification)
        {
            var codes = new List<string>();
            for (var i = 09; i < DateTime.Now.Year - 2000; i += 5)
            {
                codes.Add(string.Format("{0}{1}{2:00}", contractSpecification.Symbol, contractSpecification.Months.First(), i));
            }
            codes.Add(GetNearestExpirationMonth(contractSpecification));
            return codes;
        }


        private string GetNearestExpirationMonth(ContractSpecification contractSpecification)
        {
            var currentMonth = "FGHJKMNQUVXZ"[DateTime.Now.Month - 1];
            var selected = contractSpecification.Months.FirstOrDefault(d => currentMonth <= d);
            if (selected == '\0')
            {
                selected = contractSpecification.Months.First();
            }
            return string.Format("{0}{1}{2:00}", contractSpecification.Symbol, selected, DateTime.Now.Year - 2000);
        }

        private void OnDownload(string code)
        {
            if (Download != null)
            {
                Download(this, code);
            }
        }

    }

    
}
