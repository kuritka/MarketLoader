using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;
using MarketLoader.Configuration;
using MarketLoader.Entities;
using MarketLoader.Formatters;
using MarketLoader.Infrastructure;
using MarketLoader.Services;


namespace MarketLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            if (Parser.Default.ParseArguments(args, options))
            {
                DownloadQuotes(options);            
            }        
        }

        private static void DownloadQuotes(CommandLineOptions options)
        {
            ContractSpecification contractSpecification;
            var service = new QuotationService();            
            service.Download += Inform;
            IEnumerable<Market> markets;            
            if (options.Symbol.ToLowerInvariant() == "all")
            {
                markets = service.GetWeeklyDataForAllSymbols();
            }
            else if (ContractSpecification.TryParse(options.Symbol, out contractSpecification))
            {
                markets = new Market(contractSpecification, service.GetWeeklyData(contractSpecification.Symbol)).AsCollection();
            }
            else
            {
                WriteWrongMarketMessage();
                return;
            }
            var formatter = FormatterFactory.CreateFormatter(new FileInfo(options.OutputFile));
            formatter.Format(markets);
        }


        private static void WriteWrongMarketMessage()
        {
            Console.WriteLine("Please use one of following symbols: ");
            Console.WriteLine("{0},{1}ALL",
            ContractSpecification.Data.Select(d => string.Format("{0} ({1})", d.Symbol, d.Name))
            .Aggregate((d, s) => d + Environment.NewLine + s),Environment.NewLine);
        }

        private static void Inform(object sender, string code)
        {            
            Console.WriteLine("Downloading " + code);
        }
    }
}
