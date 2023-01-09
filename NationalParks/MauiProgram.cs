using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NationalParks.Services;

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

        builder.UseMauiApp<App>().UseMauiCommunityToolkit();

#if DEBUG
        builder.Logging.AddDebug();
#endif

    	builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		builder.Services.AddSingleton<IMap>(Map.Default);

		builder.Services.AddSingleton<DataService>();


        builder.Services.AddSingleton<AboutVM>();
        builder.Services.AddSingleton<AboutPage>();

        builder.Services.AddSingleton<DataTesterVM>();
        builder.Services.AddSingleton<DataTesterPage>();

        builder.Services.AddSingleton<ParkListVM>();
		builder.Services.AddSingleton<ParkListPage>();
        builder.Services.AddTransient<ParkDetailVM>();
		builder.Services.AddTransient<ParkDetailPage>();
        builder.Services.AddTransient<ParkFilterVM>();
        builder.Services.AddTransient<ParkFilterPage>();
        builder.Services.AddTransient<ParkImageListVM>();
        builder.Services.AddTransient<ParkImageListPage>();

        builder.Services.AddSingleton<CampgroundListVM>();
        builder.Services.AddSingleton<CampgroundListPage>();
        builder.Services.AddTransient<CampgroundDetailVM>();
        builder.Services.AddTransient<CampgroundDetailPage>();
        builder.Services.AddTransient<CampgroundFilterVM>();
        builder.Services.AddTransient<CampgroundFilterPage>();
        builder.Services.AddTransient<CampgroundImageListVM>();
        builder.Services.AddTransient<CampgroundImageListPage>();

        builder.Services.AddSingleton<TourListVM>();
        builder.Services.AddSingleton<TourListPage>();
        builder.Services.AddTransient<TourDetailVM>();
        builder.Services.AddTransient<TourDetailPage>();

        builder.Services.AddSingleton<EventListVM>();
        builder.Services.AddSingleton<EventListPage>();
        builder.Services.AddTransient<EventDetailVM>();
        builder.Services.AddTransient<EventDetailPage>();

        builder.Services.AddSingleton<PlaceListVM>();
        builder.Services.AddSingleton<PlaceListPage>();
        builder.Services.AddTransient<PlaceDetailVM>();
        builder.Services.AddTransient<PlaceDetailPage>();
        builder.Services.AddTransient<PlaceFilterVM>();
        builder.Services.AddTransient<PlaceFilterPage>();

        builder.Services.AddSingleton<WebcamListVM>();
        builder.Services.AddSingleton<WebcamListPage>();
        builder.Services.AddTransient<WebcamDetailVM>();
        builder.Services.AddTransient<WebcamDetailPage>();

        return builder.Build();
	}
}
