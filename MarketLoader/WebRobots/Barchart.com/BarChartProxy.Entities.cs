namespace MarketLoader.WebRobots.Barchart.com
{
    partial class BarChartProxy
    {

        private class BarchartQuote
        {
            public string Open { get; set; }
            public string Close { get; set; }
            public string High { get; set; }
            public string Low { get; set; }
            public string Value { get; set; }
            public string ContractType { get; set; }
            public string DateTime { get; set; }
            public string Symbol { get; set; }
        }




        private static class ContractTypes
        {
            public const string Ochc = "OCHC";
            public const string Volume = "Volume";
            public const string Interest = "Interest";
        }

    }
}
