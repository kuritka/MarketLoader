using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using MarketLoader.Entities;
using MarketLoader.Infrastructure;

namespace MarketLoader.WebRobots.Barchart.com
{

    /// <summary>
    /// Reads information from barchart.com
    /// </summary>
    internal static partial class BarChartProxy
    {

        /// <summary>
        /// Gets the historical data.
        /// </summary>        
        public static List<Quote> GetHistoricalData(string contractCode,Period periodSize, FrameSize frameSize)
        {         
            contractCode = contractCode.Replace(" ", string.Empty);
            var address = GetChartPath(contractCode, periodSize, frameSize,new List<int>());
            var xmlReader = GetXmlReader(contractCode, address);
            var ochlList = new List<BarchartQuote>();
            while (xmlReader.Read())
            {
                if (xmlReader.Name == "area")
                    ochlList.Add(ParseHistoriaclDeliverAreaNode(xmlReader));
            }
            return MakeChartPeriodList(ochlList);
        }




        private static XmlReader GetXmlReader(string contractCode, string address)
        {
            var chartData = string.IsNullOrEmpty(address) ? GetClearChartData(contractCode) : GetClearChartData(new Uri(address));
            chartData = chartData.Replace(")\">", ")\"/>");
            return XmlReader.Create(new StringReader(chartData));
        }


        /// <summary>
        /// Reads the chart with price box. over the chart
        /// Data for this chart are added inside tag chartDiv
        /// PriceBox contains open, close, min, max records
        /// Things shown inside the priceBox are shown by javascripts on the client side.
        /// !! There is css class called barchartStyle is inside, you can set how will price box looks like
        /// </summary>
        /// <param name="contractCode">The contract code.</param>
        /// <param name="periodSize">BarChartProxy.Constants.PeriodSize.Monthly next parameters are dayly, weekly</param>
        /// <param name="frameSize">Size of the frame.</param>
        /// <param name="movingAverages">add indicators higher than 1</param>
        /// <returns></returns>
        public static string GetChart(string contractCode, Period periodSize, FrameSize frameSize, List<int> movingAverages)
        {
            //http://www.barchart.com/chart.php?sym=KCZ10&indicators=
            //http://www.barchart.com/chart.php?sym={0}&style=technical{1}&d=M&sd=&ed=&size=M&log=0&t=BAR&v=2&g=1&evnt=1&late=1&o1=&o2=&o3=&sh=100{2}&txtDate=#jump            
            var address = GetChartPath(contractCode, periodSize, frameSize, movingAverages);
            return GetChart(contractCode, address,  frameSize,periodSize, movingAverages);
        }


        /// <summary>
        /// Gets the chart path with ma selective period or frame
        /// </summary>     
        private static string GetChartPath(string contractCode, Period periodSize, FrameSize frameSize, List<int> movingAverages)
        {
            var period = string.Empty;
            var indicatorString = string.Empty;
            switch (periodSize)
            {
                case Period.Daily: period = "DO"; break;
                case Period.Weekly: period = "WN"; break;
                case Period.Monthly: period = "MN"; break;
            }            
            if (!string.IsNullOrEmpty(period)) period = "&p=" + period;
            var indexes = new[] {string.Empty, "L", "O", "M", "H", "X" };
            var selectedValue = (int) frameSize;
            var frame = "&d=" +indexes[selectedValue];

            if (!movingAverages.IsNullOrEmpty())
            {
                indicatorString += "&indicators=";
                indicatorString = movingAverages.Where(indicatorSma => indicatorSma > 1).Aggregate(indicatorString, (current, indicatorSma) => current + string.Format("SMA({0},11650);", indicatorSma));
            }
            if (string.IsNullOrEmpty(contractCode)) return string.Empty;            
            var address = string.Format("http://www.barchart.com/chart.php?sym={0}&style=technical{1}{3}&sd=&ed=&size=M&log=0&t=BAR&v=2&g=1&evnt=1&late=1&sh=100{2}&txtDate=#jump", contractCode.ToUpper(), period, indicatorString,frame);
            return address;
        }        
    }     
}