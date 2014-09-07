using System;
using System.IO;
using MarketLoader.Formatters.CSharp;
using MarketLoader.Formatters.Excel;

namespace MarketLoader.Formatters
{
    internal static class FormatterFactory
    {
        public static IFormatter CreateFormatter(FileInfo file)
        {
            if (file.Extension == ".cs")
            {
                return new CSharpFormatter(file);
            }
            if (file.Extension == ".xls" || file.Extension == ".xlsx")
            {
                return new ExcelFormatter(file);
            }
            throw new NotImplementedException("Unsupported file type. Please use *.cs or *.xls");
        }
    }
}
