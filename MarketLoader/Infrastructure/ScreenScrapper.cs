using System.IO;
using System.Net;

namespace MarketLoader.Infrastructure
{
    internal static class ScreenScrapper
    {
        /// <summary>
        /// ScreenScraps http://www.4guysfromrolla.com/articles/122204-1.aspx 
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        internal static string Download(string url)
        {
            var httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
            if (httpWebRequest == null) throw new WebException(string.Format("Could not read daata from {0}. Please check your address and try it again", url));            
            httpWebRequest.Accept = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";            
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US) AppleWebKit/533.4 (KHTML, like Gecko) Chrome/5.0.375.125 Safari/533.4";            
            using (var response = httpWebRequest.GetResponse() as HttpWebResponse)
            {
                if (response == null) return string.Empty;
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {                    
                    return streamReader.ReadToEnd();
                }
            }            

        }
    }
}
