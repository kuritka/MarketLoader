using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MarketLoader.Entities;

namespace MarketLoader.Formatters.CSharp
{
    class CSharpFormatter : IFormatter
    {
        private readonly FileInfo _file;
        private readonly Regex _forbiddenCharacters = new Regex("[ &()#]");

        public CSharpFormatter(FileInfo file)
        {
            _file = file;
        }
        
        public void Format(IEnumerable<Market> markets)
        {
            File.WriteAllLines(_file.FullName, FormatQuotes(markets));
        }

        private IEnumerable<string> FormatQuotes(IEnumerable<Market> markets)
        {
            var lines = new List<string>();
            lines.Add("using System;");
            lines.Add("using System.Collections.Generic;");
            lines.Add("using System.Linq;");
            lines.Add("namespace Futures {");            
            lines.Add("class Data");
            lines.Add("{");
            foreach (var market in markets)
            {
                lines.AddRange( FormatQuotes(market.Quotes, market.ContractSpecification));
            }
            lines.AddRange(FormatMarketContainer(markets));
            lines.Add(IncludeNestedEntities());
            lines.Add("}");
            lines.Add("}");
            return lines;
        }

        private string IncludeNestedEntities()
        {
            return Resource.NestedEntities;
        }

        private IEnumerable<string> FormatMarketContainer(IEnumerable<Market> markets)
        {
            var lines = new List<string>();
            lines.Add("public static List<Market> MarketContainer");
            lines.Add("{");
            lines.Add(" get {");
            lines.Add("var markets = new List<Market>();");
            lines.AddRange(
                markets.Select(d => string.Format("markets.Add({0});", 
                    _forbiddenCharacters.Replace(d.ContractSpecification.Name, string.Empty))));
            lines.Add(" return markets;");
            lines.Add(" }");
            lines.Add("}");
            return lines;
            
        }


        private IEnumerable<string>  FormatQuotes(IEnumerable<Quote> quotes, ContractSpecification contractSpecification)
        {
            var lines = new List<string>();
            lines.Add(string.Format("public static Market {0}", _forbiddenCharacters.Replace(contractSpecification.Name, string.Empty)));
            lines.Add("{");
            lines.Add("\tget {");
            lines.Add("\t return new Market(");
            lines.Add(string.Format("new ContractSpecification(\"{0}\",\"{1}\",{2},\"{3}\",\"{4}\",Category.{5},{6},\"{7}\"),",
                contractSpecification.Exchange, contractSpecification.Months, 
                contractSpecification.PointValueUsd, contractSpecification.Symbol, 
                contractSpecification.Name,contractSpecification.Category,
                contractSpecification.ContractSize, contractSpecification.SizeUnit));
            
            lines.Add("new List<Quote>{ ");
            foreach (var quote in quotes)
            {
                lines.Add(string.Format(
                    "\t\tnew Quote{{Symbol = \"{0}\",DateTime=new DateTime({1},{2},{3}),Close ={4},High = {5},Interest = {6},Low = {7},Open = {8},Period=Period.{9},Volume = {10}}},"
                    , quote.Symbol, quote.DateTime.Year, quote.DateTime.Month, quote.DateTime.Day, quote.Close, quote.High, quote.Interest,
                    quote.Low, quote.Open, Period.Weekly, quote.Volume));
            }
            lines.Add("\t\t});");            
            lines.Add("\t}");
            lines.Add("}");
            return lines;
        }    
    }
}
