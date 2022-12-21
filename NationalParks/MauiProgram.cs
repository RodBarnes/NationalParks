using Microsoft.Extensions.Logging;
using NationalParks.Services;
using NationalParks.Views;

namespace NationalParks;

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
#if DEBUG
		builder.Logging.AddDebug();
#endif

    	builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		builder.Services.AddSingleton<IMap>(Map.Default);

		builder.Services.AddSingleton<DataService>();

		builder.Services.AddSingleton<ParkListVM>();
		builder.Services.AddSingleton<ParkListPage>();

		builder.Services.AddTransient<ParkDetailVM>();
		builder.Services.AddTransient<ParkDetailPage>();

        builder.Services.AddTransient<FilterVM>();
        builder.Services.AddTransient<FilterPage>();

        builder.Services.AddTransient<HoursVM>();
        builder.Services.AddTransient<HoursPage>();

        builder.Services.AddTransient<ImagesVM>();
        builder.Services.AddTransient<ImagesPage>();

        return builder.Build();
	}
}
