using System;
using CommandLine;
using CommandLine.Text;

namespace MarketLoader.Configuration
{
    internal class CommandLineOptions
    {

        [Option('s', "symbol", Required = true, HelpText = "Symbol you want to download i.e. KC for Arabica coffee, CC for cocoa, ALL for all symbols. ")]
        public string Symbol { get; set; }

        [Option('p', "Period", DefaultValue = "Weekly", Required = false, HelpText = "Period {Weekly, Daily};")]
        public string Period { get; set; }
        
        [Option('o', "OutputFile", Required = true, HelpText = "local file output i.e. Quotes.cs")]
        public string OutputFile { get; set; }

        
        [HelpOption]
        public string GetUsage()
        {
            return string.Format("{0}{1}Example of use: MarketLoader.exe -all -o quotes.cs{1}", 
                HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current)), 
                Environment.NewLine);
        }


    }
}
