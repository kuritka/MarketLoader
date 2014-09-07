using System.Collections.Generic;
using System.Linq;

namespace MarketLoader.Entities
{
    public partial class ContractSpecification
    {

        public static IReadOnlyCollection<ContractSpecification> Data
        {
            get
            {
                return new List<ContractSpecification>
                {
                    new ContractSpecification("CME", "HMUZ", 100000M, "A6", "Australian Dollar", Category.Financials,
                        100000, "AUD"),
                    new ContractSpecification("CBOT", "FHKNQUVZ", 600M, "BO", "Soybean Oil (P)",
                        Category.GrainsAndOilseeds, 60000, "lb"),
                    new ContractSpecification("CBOT", "HKNUZ", 50M, "C ", "Corn (P)", Category.GrainsAndOilseeds, 5000,
                        "bu"),
                    new ContractSpecification("ICE", "FGHJKMNQUVXZ", 1000M, "CB", "Crude Oil Brent", Category.Energies,
                        1000, "bbl"),
                    new ContractSpecification("ICEUS", "HKNUZ", 10M, "CC", "Cocoa", Category.Softs, 10, "ton"),
                    new ContractSpecification("NYMEX", "FGHJKMNQUVXZ", 1000M, "CL", "Crude Oil", Category.Energies, 1000,
                        "bbl"),
                    new ContractSpecification("ICEUS", "HKNVZ", 500M, "CT", "Cotton #2", Category.Softs, 50000, "lb"),
                    new ContractSpecification("CME", "HMUZ", 100000M, "D6", "Canadian Dollar", Category.Financials,
                        100000, "CAD"),
                    new ContractSpecification("CME", "FGHJKMNQUVXZ", 2000M, "DA", "Class III Milk (P)", Category.Meats,
                        200000, "lb"),
                    new ContractSpecification("CME", "FGHJKMNQUVXZ", 2000M, "DL", "Class III Milk", Category.Meats,
                        200000, "lb"),
                    new ContractSpecification("ICEFI", "HMUZ", 1000M, "DX", "US Dollar Index", Category.Financials, 1000,
                        "USD"),
                    new ContractSpecification("CME", "HMUZ", 125000M, "E6", "Euro FX", Category.Financials, 125000,
                        "EUR"),
                    new ContractSpecification("CME", "FHJKQUVX", 500M, "FC", "Feeder Cattle (P)", Category.Meats, 50000,
                        "lb"),
                    new ContractSpecification("COMEX", "GJMQVZ", 100M, "GC", "Gold", Category.Metals, 100, "ozt"),
                    new ContractSpecification("CME", "FHJKQUVX", 500M, "GF", "Feeder Cattle", Category.Meats, 50000,
                        "lb"),
                    new ContractSpecification("CBOT", "GJKMNQVZ", 400M, "HE", "Lean Hoghs", Category.Meats, 40000, "lb"),
                    new ContractSpecification("COMEX", "FGHJKMNQUVXZ", 25000M, "HG", "High Grade Cooper",
                        Category.Metals, 25000, "lb"),
                    new ContractSpecification("NYMEX", "FGHJKMNQUVXZ", 42000M, "HO", "Heating Oil", Category.Energies,
                        42000, "gal"),
                    new ContractSpecification("ICEUS", "HKNUZ", 375M, "KC", "Coffee Arabica", Category.Softs, 37500,
                        "lb"),
                    new ContractSpecification("KCBT", "HKNUZ", 50M, "KE", "Red Wheat", Category.GrainsAndOilseeds, 5000,
                        "bu"),
                    new ContractSpecification("KCBT", "HKNUZ", 50M, "KW", "Kcbt Red Wheat (P) ",
                        Category.GrainsAndOilseeds, 5000, "bu"),
                    new ContractSpecification("CME", "FHKNUX", 110M, "LB", "Timber", Category.Softs, 110000, "bd ft"),
                    new ContractSpecification("CME", "GJMQVZ", 400M, "LC", "Live Cattle (P)", Category.Meats, 40000,
                        "lb"),
                    new ContractSpecification("CME", "GJMQVZ", 400M, "LE", "Live Cattle", Category.Meats, 40000, "lb"),
                    new ContractSpecification("CME", "GJKMNQVZ", 400M, "LH", "Lean Hogs", Category.Meats, 40000, "lb"),
                    new ContractSpecification("CME", "FHKNUX", 110M, "LS", "Lumber", Category.Softs, 110000, "bd ft"),
                    new ContractSpecification("MGEX", "HKNUZ", 50M, "MW", "Spring Wheat", Category.GrainsAndOilseeds,
                        5000, "bu"),
                    new ContractSpecification("NYMEX", "FGHJKMNQUVXZ", 10000M, "NG", "Natural Gas", Category.Energies,
                        10000, "MMBtu"),
                    new ContractSpecification("CBOT", "HKNUZ", 50M, "O ", "Oats (P)", Category.GrainsAndOilseeds, 5000,
                        "bu"),
                    new ContractSpecification("ICEUS", "FHKNUX", 150M, "OJ", "Orange Juice", Category.Softs, 15000, "lb"),
                    new ContractSpecification("NYMEX", "HJKMUZ", 100M, "PA", "Palladium", Category.Metals, 100, "ozt"),
                    new ContractSpecification("CME", "GHKNQ", 400M, "PB", "Pork Bellies (P)", Category.Meats, 40000,
                        "lb"),
                    new ContractSpecification("CME", "GHKNQ", 400M, "PD", "Pork Bellies", Category.Meats, 40000, "lb"),
                    new ContractSpecification("NYMEX", "FHJKNV", 50M, "PL", "Platinum", Category.Metals, 50, "ozt"),
                    new ContractSpecification("NYMEX", "FGHJKMNQUVXZ", 42000M, "RB", "Gasoline RBOB", Category.Energies,
                        42000, "gal"),
                    new ContractSpecification("LCE", "FHKNUX", 10M, "RM", "Coffee Robusta", Category.Softs, 10, "ton"),
                    new ContractSpecification("CBOT", "FHKNUX", 2000M, "RR", "Rough Rice (P)",
                        Category.GrainsAndOilseeds, 2000, "cwt"),
                    new ContractSpecification("WCE", "FHKNX", 20M, "RS", "Canola", Category.GrainsAndOilseeds, 20, "ton"),
                    new ContractSpecification("CBOT", "FHKNQUX", 50M, "S ", "Soybeans (P)", Category.GrainsAndOilseeds,
                        5000, "bu"),
                    new ContractSpecification("CME", "HMUZ", 125000M, "S6", "Swiss Franc", Category.Financials, 125000,
                        "CHF"),
                    new ContractSpecification("ICEUS", "HKNV", 1120M, "SB", "Sugar#11", Category.Softs, 112000, "lb"),
                    new ContractSpecification("ICEUS", "FHKNUX", 1120M, "SD", "Sugar #16", Category.Softs, 112000, "lb"),
                    new ContractSpecification("IOM", "FHHKMNUUZZ", 5000M, "SI", "Silver", Category.Metals, 5000, "ozt"),
                    new ContractSpecification("CBOT", "FHKNQUVZ", 100M, "SM", "Soybean Meal (P)",
                        Category.GrainsAndOilseeds, 100, "ton"),
                    new ContractSpecification("CME", "HMUZ", 250M, "SP", "S&P 500 Index", Category.Indices, 250, "USD"),
                    new ContractSpecification("CFE", "FGHJKMNQUVXZ", 1000M, "VI", "Cboe S&P VIX", Category.Indices, 1000,
                        "USD"),
                    new ContractSpecification("CBOT", "HKNUZ", 50M, "W ", "Wheat (P)", Category.GrainsAndOilseeds, 5000,
                        "bu"),
                    new ContractSpecification("CBOT", "HKNUZ", 50M, "ZC", "Corn", Category.GrainsAndOilseeds, 5000, "bu"),
                    new ContractSpecification("CBOT", "FGHJKMNQUVXZ", 29000M, "ZK", "Ethanol", Category.Energies, 29000,
                        "gal"),
                    new ContractSpecification("CBOT", "FHKNQUVZ", 600M, "ZL", "Soybean Oil", Category.GrainsAndOilseeds,
                        60000, "lb"),
                    new ContractSpecification("CBOT", "FHKNQUVZ", 100M, "ZM", "Soybean Meal", Category.GrainsAndOilseeds,
                        100, "ton"),
                    new ContractSpecification("CBOT", "HKNUZ", 50M, "ZO", "Oats", Category.GrainsAndOilseeds, 5000, "bu"),
                    new ContractSpecification("CBOT", "FHKNUX", 2000M, "ZR", "Rough Rice", Category.GrainsAndOilseeds,
                        2000, "cwt"),
                    new ContractSpecification("CBOT", "FHKNQUX", 50M, "ZS", "Soybeans", Category.GrainsAndOilseeds, 5000,
                        "bu"),
                    new ContractSpecification("CBOT", "HKNUZ", 50M, "ZW", "Wheat", Category.GrainsAndOilseeds, 5000,
                        "bu")
                };
            }
        }

        public static bool TryParse(string symbol, out ContractSpecification contractSpecification)
        {
            contractSpecification = Data.FirstOrDefault(d => d.Symbol == symbol.ToUpperInvariant());
            return contractSpecification != null;
        }
    }



}
