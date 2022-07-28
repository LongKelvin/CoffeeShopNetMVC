using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

[assembly: FunctionsStartup(typeof(CoffeeShop.PdfGenerator.Startup))]

namespace CoffeeShop.PdfGenerator
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        }
    }
}