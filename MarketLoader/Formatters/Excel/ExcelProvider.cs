using System.Collections.Generic;
using System.IO;

namespace MarketLoader.Formatters.Excel
{
  
    public static class ExcelProvider
    {
        
        /// <summary>
        /// Creates excel sheet stream         
        /// </summary>                
        public static MemoryStream GetExcel<T>(ExcelType excelType, IEnumerable<T> list, string title = "")
        {            
            return GetStream(excelType, list, title);
        }


        private static MemoryStream GetStream<T>(ExcelType excelType, IEnumerable<T> list, string title)
        {
            var ms = new MemoryStream();
            Generator.ExcelGenerator.BuildWorkbook(excelType, ms, list, title);
            ms.Seek(0, 0);
            return ms;
        }
    }
}
