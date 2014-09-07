using System.Collections.Generic;

namespace MarketLoader.Entities
{
    public class Market
    {
        public Market(ContractSpecification contractSpecification, IEnumerable<Quote> quotes)
        {
            ContractSpecification = contractSpecification;
            Quotes = quotes;
        }

        public ContractSpecification ContractSpecification { get; private set; }

        public IEnumerable<Quote> Quotes { get; private set; }
    }
}
