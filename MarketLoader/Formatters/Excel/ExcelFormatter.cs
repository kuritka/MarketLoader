using System.Collections.Generic;
using System.IO;
using System.Linq;
using MarketLoader.Entities;

namespace MarketLoader.Formatters.Excel
{
    internal class ExcelFormatter : IFormatter
    {
        private readonly FileInfo _file;

        public ExcelFormatter(FileInfo file)
        {
            _file = file;
        }

        public void Format(IEnumerable<Market> markets)
        {                        
            var excel = ExcelProvider.GetExcel(ExcelType.CeexWithLogo, markets.SelectMany(d => d.Quotes), "Quotations");
            File.WriteAllBytes(_file.FullName, excel.ToArray());                     
        }
    }
}
