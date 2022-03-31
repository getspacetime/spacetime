using Microsoft.AspNetCore.Components.WebView.Maui;
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
			.RegisterBlazorMauiWebView()
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddBlazorWebView();
		builder.Services.AddSingleton<RequestService>();
		builder.Services.AddSingleton<SpacetimeRestService>();
		builder.Services.AddSingleton<IGrpcExplorer, GrpcExplorer>();

		return builder.Build();
	}
}
