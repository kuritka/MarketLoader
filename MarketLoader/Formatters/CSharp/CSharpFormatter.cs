using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using MarketLoader.Entities;

namespace MarketLoader.Formatters.CSharp
{
    class CSharpFormatter : IFormatter
    {
        private readonly FileInfo _file;

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
            lines.Add("class Quotes");
            lines.Add("{");
            foreach (var market in markets)
            {
                lines.AddRange( FormatQuotes(market.Quotes, market.ContractSpecification));
            }
            lines.Add("}");
            return lines;
        }


        private IEnumerable<string>  FormatQuotes(IEnumerable<Quote> quotes, ContractSpecification contractSpecification)
        {
            var lines = new List<string>();
            lines.Add(string.Format("public IList<Quote> {0}", new Regex("[ &()#]").Replace(contractSpecification.Name, string.Empty)));
            lines.Add("{");
            lines.Add("\tget {");
            lines.Add("\treturn new List<Quote>{ ");
            foreach (var quote in quotes)
            {
                lines.Add(string.Format(
                    "\t\tnew Quote{{Symbol = \"{0}\",DateTime=new DateTime({1},{2},{3}),Close ={4},High = {5},Interest = {6},Low = {7},Open = {8},Period=Period.{9},Volume = {10}}},"
                    , quote.Symbol, quote.DateTime.Year, quote.DateTime.Month, quote.DateTime.Day, quote.Close, quote.High, quote.Interest,
                    quote.Low, quote.Open, Period.Weekly, quote.Volume));
            }
            lines.Add("\t};");
            lines.Add("\t}");
            lines.Add("}");
            return lines;
        }    
    }
}
