using Microsoft.AspNetCore.Components.WebView.Maui;
using FastGamePanel.Data;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using FastGamePanel.Services;

namespace FastGamePanel;
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

		builder.Services.AddBlazorWebViewDeveloperTools();

		builder.Services.AddLogging();

        builder.Services.AddSingleton<WeatherForecastService>();

		builder.Services.AddSingleton<DisplaySettingService>();

		builder.Services.AddSingleton<PowerSettingService>();

        return builder.Build();
	}
}
