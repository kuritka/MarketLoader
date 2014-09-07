using System.Collections.Generic;
using System.IO;
using MarketLoader.Entities;


namespace MarketLoader.Formatters
{
    interface IFormatter
    {
        void Format(IEnumerable<Market> markets);
    }
}
