using Spacetime.Core;
using Spacetime.Core.gRPC;
using Spacetime.Core.Services;

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

		builder.Services.AddMauiBlazorWebView();
		builder.Services.AddBlazorWebView();
		builder.Services.AddSingleton<RequestService>();
		builder.Services.AddSingleton<SettingsService>();
		builder.Services.AddSingleton<SpacetimeRestService>();
		builder.Services.AddSingleton<UrlBuilder>();
		builder.Services.AddSingleton<IGrpcExplorer, GrpcExplorer>();

		//builder.Services.AddHttpClient("", client =>
		//{

		//});

		return builder.Build();
	}
}
