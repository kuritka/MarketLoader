using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using MarketLoader.Entities;
using MarketLoader.Infrastructure;

namespace MarketLoader.WebRobots.Barchart.com
{
    partial class BarChartProxy
    {
        // Sorry barchart.com :D
        private  string GetChart(string contractCode, string address, FrameSize frameSize, Period periodSize, List<int> map)
        {
            try
            {
                var chart = GetClearChartData(new Uri(address));
                var imageName = GetPictureName(chart);
                var mapList = string.Empty;
                if (!map.IsNullOrEmpty()) mapList = string.Format("{0}", map.Aggregate((s, d) => s + ',' + d));
                chart = chart.Replace(Constants.BarchartCache + imageName, string.Format("/WebParts/ChartData/ChartImage.aspx?_imageName={0}&_contractCode={1}&_frame={2}&_period={3}&_ma={4}", imageName, contractCode,frameSize,periodSize,mapList));
                return "<div  id=\"chartpricebox\"  class=\"barchartStyle\">Move cursor over the chart </div>" + chart;
            }
            catch
            {
                return "<div id=\"chartdiv\"/>";
            }
        }


        private  BarchartQuote ParseHistoriaclDeliverAreaNode(XmlReader xmlReader)
        {
            var contractDataText = xmlReader.GetAttribute("onmousemove");
            if (string.IsNullOrEmpty(contractDataText)) return new BarchartQuote();
            contractDataText = contractDataText.Replace("'", string.Empty);
            return MakeStockItemDataFromStockItemArray(contractDataText);
        }

        private  BarchartQuote MakeStockItemDataFromStockItemArray(string areaText)
        {
            var crate = new BarchartQuote();
            if (areaText.StartsWith("showOHLCTooltip(event,") || areaText.StartsWith("showStudyTooltip(event,"))
            {
                var par = areaText.Substring(areaText.IndexOf('['), areaText.IndexOf(']') - areaText.IndexOf('['));
                areaText = areaText.Replace(par, par.Replace(",", string.Empty));
                var splitted = areaText.Replace("showOHLCTooltip(event,", string.Empty).Replace("showStudyTooltip(event,",string.Empty).Replace(")", string.Empty).Split(',');
                crate.DateTime = splitted[1].TrimStart(' ', '[').TrimEnd(']');
                switch (splitted[2].Trim())
                {
                    case ContractTypes.Volume:
                        crate.ContractType = ContractTypes.Volume;
                        crate.Value = splitted[3];                                                
                        break;
                    case ContractTypes.Interest:
                        crate.ContractType = ContractTypes.Interest;
                        crate.Value = splitted[3];
                        break;
                    default:
                        crate.Symbol = splitted[2].Trim();                        
                        crate.Open= splitted[3];
                        crate.High = splitted[4];
                        crate.Low = splitted[5];
                        crate.Close = splitted[6];
                        crate.ContractType = ContractTypes.Ochc;
                        break;
                }
            }
            return crate;
        }

        

        /// <summary>
        /// Makes list of final period items from crates.
        /// there are 4x more crates then final items
        /// </summary>        
        private  List<Quote> MakeChartPeriodList(IEnumerable<BarchartQuote> crates)
        {
            var chartPeriodList = new List<Quote>();
            var ochcCrates = crates.GroupBy(d => d.DateTime);

            foreach (var crate in ochcCrates)
            {
                var item = new Quote();
                foreach (var itemCrate in crate)
                {
                    switch (itemCrate.ContractType)
                    {
                        case ContractTypes.Ochc:
                            item.Close = TryParseDouble(itemCrate.Close);
                            item.DateTime = TryParseDateTime(itemCrate.DateTime);
                            item.High = TryParseDouble(itemCrate.High);
                            item.Low = TryParseDouble(itemCrate.Low);
                            item.Open = TryParseDouble(itemCrate.Open);
                            item.Symbol = itemCrate.Symbol;
                            break;
                        case ContractTypes.Volume:
                            var volume = TryParseInt(itemCrate.Value);
                            if(volume != 0) item.Volume = volume;
                            break;
                        case ContractTypes.Interest:
                            item.Interest = TryParseInt(itemCrate.Value);
                            break;
                    }

                }
                chartPeriodList.Add(item);
            }
            return chartPeriodList;
        }


        /// <summary>
        /// Gets the name of the picture.
        /// </summary>        
        private  string GetPictureName(string chartData)
        {
            var imgId = chartData.Remove(0, chartData.IndexOf("cache/", StringComparison.Ordinal) + 6);
            return imgId.Substring(0, imgId.IndexOf("\"", StringComparison.Ordinal));
        }


        /// <summary>
        /// Gets the chart.
        /// </summary>
        /// <param name="contractCode">The contract code.</param>
        /// <returns></returns>
        /// http://www.codersource.net/microsoft-net/c-advanced/html-screen-scraping-in-c.aspx
        /// http://referer.us/hide-http-referer.html#    
        /// http://net.tutsplus.com/tutorials/other/http-headers-for-dummies/
        private static string GetClearChartData(string contractCode)
        {            
            var address = string.Format("http://www.barchart.com/charts/futures/{0}", contractCode.ToUpper());
            return GetClearChartData(new Uri(address));            
        }

        
        /// <summary>
        /// Gets the chart.
        /// </summary>        
        /// http://www.codersource.net/microsoft-net/c-advanced/html-screen-scraping-in-c.aspx
        /// http://referer.us/hide-http-referer.html#
        /// http://net.tutsplus.com/tutorials/other/http-headers-for-dummies/
        //private static string GetClearChartData(string contractCode, string  address)
        private static string GetClearChartData(Uri address)
        {
            try
            {
                var barchartPage = ScreenScrapper.Download(address.AbsoluteUri);
                var start = barchartPage.IndexOf("<div id=\"chartdiv\">", StringComparison.Ordinal);
                var end = barchartPage.IndexOf("<img src=\"data:image/png;base64", StringComparison.Ordinal);                
                var chart = barchartPage.Substring(start, end - start);
                chart += "</div>";
                chart = chart.Replace("/cache/", Constants.BarchartCache);
                chart = chart.Replace("</center>", string.Empty);

                return chart;
            }
            catch
            {
                return "<div id=\"chartdiv\"/>";
            }
        }
        

     
        private static int TryParseInt(string value)
        {
            try
            {
                var number = (int)Decimal.Parse(value, CultureInfo.InvariantCulture);
                return number;
            }
            catch
            {
                return 0;
            }
        }


        private static double TryParseDouble(string value)
        {
            try
            {
                var number = Double.Parse(value, CultureInfo.InvariantCulture);
                return number;
            }
            catch
            {
                return 0;
            }
        }


        private static DateTime TryParseDateTime(string value)
        {
            value = value.Replace("  ", " ");
            value = value.Replace("Mon.", string.Empty).Replace("Tue.", string.Empty).Replace("Wed.", string.Empty).Replace("Thu.", string.Empty).Replace("Fri.", string.Empty).Replace("Sat.", string.Empty).Replace("Sun.", string.Empty).TrimStart().TrimEnd();
            DateTime d;
            var p = DateTime.TryParseExact(value, new[] { "MMM dd, yyyy", "MMM d, yyyy", "MMM dd yyyy", "MMM d yyyy", "MMM dd", "MMM d", "MMM yyyy", "MMM yy", "MMM y" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
            if (!p) return new DateTime();
            if (d.Year == DateTime.MinValue.Year) d = new DateTime(DateTime.Now.Year, d.Month, d.Day);
            return d;
        }
    }
}