using Microsoft.AspNetCore.Components.WebView.Maui;
using Spacetime.Core;

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

		return builder.Build();
	}
}
