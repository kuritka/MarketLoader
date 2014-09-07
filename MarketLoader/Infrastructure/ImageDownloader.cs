using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace MarketLoader.Infrastructure
{
    /// <summary>
    /// Class is responsible or downloading images
    /// </summary>
    internal class ImageDownloader
    {
            private readonly string _imageUrl;
            private Bitmap _bitmap;
            private static bool _b;

            public ImageDownloader(string imageUrl)
            {
                _imageUrl = imageUrl;
                _b = true;
            }


            /// <summary>
            /// Downloads this instance.
            /// </summary>            
            public bool Download()
            {
                var client = new WebClient();
                try
                {
                    using (var stream = client.OpenRead(_imageUrl))
                    {
                        _bitmap = new Bitmap(stream);
                    }
                    return true;
                }
                //try it once again when we are not succesfull
                catch (WebException)
                {
                    if (_b)
                    {
                        _b = false;
                        return Download();
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

          
            public void SaveImage(Stream stream, ImageFormat format)
            {
                if (_bitmap != null)
                {
                    _bitmap.Save(stream, format);
                }
            }
    }
}
