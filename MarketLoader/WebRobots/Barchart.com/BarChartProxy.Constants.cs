namespace MarketLoader.WebRobots.Barchart.com
{
    partial class BarChartProxy
    {
        static class Constants
        {
            internal const string BarchartCache = "http://www.barchart.com/cache/";
        }

        
        /// <summary>        
        /// Horizon size. 
        /// </summary>
        public enum FrameSize
        {
            /// <summary>
            /// 5 let/ 1 rok / 2 mesice  v zavislosti na period size
            /// </summary>
            Smaller = 1,
            /// <summary>
            ///  7 let / 2 roky / 4 mesice v zavislosti na period size
            /// </summary>
            Small = 2,
            /// <summary>
            ///  10 let / 3 roky / 6 mesice v zavislosti na period size
            /// </summary>
            Normal = 3,
            /// <summary>
            ///  15 let / 4 roky / 8 mesicu v zavislosti na period size
            /// </summary>
            Large = 4,
            /// <summary>
            ///  25 let / 5 let / 1 rok v zavislosti na period size
            /// </summary>
            Largest = 5            
        }

    }
}
