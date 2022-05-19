using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;
using Spacetime.Blazor.gRPC.Store;
using Spacetime.CompositionRoot;
using Spacetime.Web;

namespace Spacetime.Web.Client;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        // set up logging with Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Debug()
            .CreateLogger();

        builder.Services.AddLogging(options =>
        {
            options.AddSerilog(dispose: true);
        });

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


        // add third party libraries
        builder.Services.AddFluxor(options => options.ScanAssemblies(
            //typeof(RequestState).Assembly, 
            typeof(GrpcState).Assembly));

        builder.Services.AddSpacetime();

        // use Autofac integration
        builder.ConfigureContainer(new AutofacServiceProviderFactory(ConfigureContainer));

        await builder.Build().RunAsync();
    }

    private static void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule<WebModule>();
        builder.RegisterModule<CoreModule>();
    }
}
