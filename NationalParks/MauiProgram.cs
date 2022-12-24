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


        builder.Services.AddSingleton<AboutVM>();
        builder.Services.AddSingleton<AboutPage>();

        
		builder.Services.AddSingleton<ParkListVM>();
		builder.Services.AddSingleton<ParkListPage>();
        builder.Services.AddTransient<ParkDetailVM>();
		builder.Services.AddTransient<ParkDetailPage>();
        builder.Services.AddTransient<ParkFilterVM>();
        builder.Services.AddTransient<ParkFilterPage>();
        builder.Services.AddTransient<ParkHoursVM>();
        builder.Services.AddTransient<ParkHoursPage>();
        builder.Services.AddTransient<ParkImageListVM>();
        builder.Services.AddTransient<ParkImageListPage>();

        builder.Services.AddSingleton<CampgroundListVM>();
        builder.Services.AddSingleton<CampgroundListPage>();
        builder.Services.AddTransient<CampgroundDetailVM>();
        builder.Services.AddTransient<CampgroundDetailPage>();
        builder.Services.AddTransient<CampgroundHoursVM>();
        builder.Services.AddTransient<CampgroundHoursPage>();
        builder.Services.AddTransient<CampgroundImageListVM>();
        builder.Services.AddTransient<CampgroundImageListPage>();

        builder.Services.AddSingleton<WebcamListVM>();
        builder.Services.AddSingleton<WebcamListPage>();
        builder.Services.AddTransient<WebcamDetailVM>();
        builder.Services.AddTransient<WebcamDetailPage>();

        return builder.Build();
	}
}
