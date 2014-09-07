namespace MarketLoader.Entities
{
    public partial class ContractSpecification
    {
        public ContractSpecification(string exchange, string months, decimal pointValueUsd,
                                        string symbol, string name, Category category,
                                        decimal contractSize, string sizeUnit)
        {
            Exchange = exchange;
            Months = months;
            PointValueUsd = pointValueUsd;
            Symbol = symbol;
            Name = name;
            Category = category;
            ContractSize = contractSize;
            SizeUnit = sizeUnit;
        }

        public string Name { get; private set; }

        public string SizeUnit { get; private set; }

        public decimal ContractSize { get; private set; }

        public Category Category { get; private set; }

        public string Exchange { get; private set; }

        public string Months { get; private set; }

        public decimal PointValueUsd { get; private set; }

        public string Symbol { get; private set; }
    }    
}
