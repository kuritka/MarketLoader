using System.Collections.Generic;
using MarketLoader.Entities;

namespace MarketLoader.Infrastructure
{
    internal static class Extensions
    {
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }
        
        public static void AddRange(this HashSet<Quote> destination, List<Quote> range)
        {
            range.ForEach(item => destination.Add(item));                
        }
        
        public static IEnumerable<T> AsCollection<T>(this T item)
        {
            return new List<T> {item};
        }
    }
}
