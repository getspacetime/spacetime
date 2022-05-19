using Autofac;
using Autofac.Extensions.DependencyInjection;
using Spacetime.Core;
using Spacetime.Core.gRPC;
using Spacetime.Core.Services;
using Spacetime.Helpers;
using MudBlazor.Services;
using Serilog;
using Fluxor;
using Spacetime.Blazor.gRPC.Store;
using Spacetime.Blazor.Shared;
using Spacetime.Blazor.Shared.Themes;
using Spacetime.CompositionRoot;
using Spacetime.Core.Formatters;
using Spacetime.Core.gRPC.Dynamic;
using Spacetime.Core.gRPC.Interfaces;
using Spacetime.Settings;
using Spacetime.Store.Requests;

namespace Spacetime;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

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

        // add maui/hybrid dependencies
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Services.AddBlazorWebView();
        
        // add third party libraries
        builder.Services.AddFluxor(options => options.ScanAssemblies(
            typeof(RequestState).Assembly, 
            typeof(GrpcState).Assembly));

        builder.Services.AddSpacetime();

        // use Autofac integration
        builder.ConfigureContainer(new AutofacServiceProviderFactory(ConfigureContainer));

        return builder.Build();
    }

    private static void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule<WebModule>();
        builder.RegisterModule<CoreModule>();
    }
}