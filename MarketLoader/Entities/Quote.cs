using System;
using MarketLoader.Formatters.Excel;

namespace MarketLoader.Entities
{
    public class Quote 
    {
        public Quote()
        {
            Period = Period.Unknown;
        }

        [Column("Code",10)]
        public string Symbol { get; set; }
        
        public Period Period { get; set; }

        [Column("Date", 15)]
        public DateTime DateTime { get; set; }

        [Column("Open", 10)]
        public double Open { get; set; }

        [Column("Close", 10)]
        public double Close { get; set; }

        [Column("High", 10)]
        public double High { get; set; }

        [Column("Low", 10)]
        public double Low { get; set; }

        [Column("Volume", 15)]
        public int Volume { get; set; }

        [Column("Interest", 15)]
        public int Interest { get; set; }


        public override int GetHashCode()
        {
            return string.Format("{0}{1}",Symbol,DateTime.ToShortDateString()).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }
    }
}