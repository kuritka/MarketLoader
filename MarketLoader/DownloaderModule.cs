using System;
using System.IO;
using MarketLoader.Configuration;
using MarketLoader.Formatters;
using MarketLoader.Services;
using MarketLoader.WebRobots.Barchart.com;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace MarketLoader
{
    internal class DownloaderModule : INinjectModule
    {
        private CommandLineOptions _options;

        public DownloaderModule(CommandLineOptions options)
        {
            _options = options;
        }

        public IKernel Kernel { get; private set; }
        public void OnLoad(IKernel kernel)
        {            
            kernel.Bind<IRobotProxy>().To<BarChartProxy>();
            kernel.Bind<FileInfo>().ToSelf().WithParameter(new ConstructorArgument("fileName", _options.OutputFile));
            kernel.Bind<FormatterFactory>().ToSelf();        
            kernel.Bind<IService>().To<QuotationService>();
        }

        public void OnUnload(IKernel kernel)
        {
            throw new NotImplementedException();
        }

        public void OnVerifyRequiredModules()
        {        
        }

        public string Name { get { return "DownloaderModule"; }  }
    }
}
