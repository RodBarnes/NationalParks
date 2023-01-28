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

        builder.Services.AddSingleton<ImageListVM>();
        builder.Services.AddSingleton<ImageListPage>();

        builder.Services.AddSingleton<ParkListVM>();
		builder.Services.AddSingleton<ParkListPage>();
        builder.Services.AddTransient<ParkDetailVM>();
		builder.Services.AddTransient<ParkDetailPage>();
        builder.Services.AddTransient<ParkFilterVM>();
        builder.Services.AddTransient<ParkFilterPage>();

        builder.Services.AddSingleton<CampgroundListVM>();
        builder.Services.AddSingleton<CampgroundListPage>();
        builder.Services.AddTransient<CampgroundDetailVM>();
        builder.Services.AddTransient<CampgroundDetailPage>();
        builder.Services.AddTransient<CampgroundFilterVM>();
        builder.Services.AddTransient<CampgroundFilterPage>();

        builder.Services.AddSingleton<TourListVM>();
        builder.Services.AddSingleton<TourListPage>();
        builder.Services.AddTransient<TourDetailVM>();
        builder.Services.AddTransient<TourDetailPage>();
        builder.Services.AddTransient<TourFilterVM>();
        builder.Services.AddTransient<TourFilterPage>();

        builder.Services.AddSingleton<PlaceListVM>();
        builder.Services.AddSingleton<PlaceListPage>();
        builder.Services.AddTransient<PlaceDetailVM>();
        builder.Services.AddTransient<PlaceDetailPage>();
        builder.Services.AddTransient<PlaceFilterVM>();
        builder.Services.AddTransient<PlaceFilterPage>();

        builder.Services.AddSingleton<ThingToDoListVM>();
        builder.Services.AddSingleton<ThingToDoListPage>();
        builder.Services.AddTransient<ThingToDoDetailVM>();
        builder.Services.AddTransient<ThingToDoDetailPage>();
        builder.Services.AddTransient<ThingToDoFilterVM>();
        builder.Services.AddTransient<ThingToDoFilterPage>();

        builder.Services.AddSingleton<EventListVM>();
        builder.Services.AddSingleton<EventListPage>();
        builder.Services.AddTransient<EventDetailVM>();
        builder.Services.AddTransient<EventDetailPage>();
        builder.Services.AddTransient<EventFilterVM>();
        builder.Services.AddTransient<EventFilterPage>();

        builder.Services.AddSingleton<WebcamListVM>();
        builder.Services.AddSingleton<WebcamListPage>();
        builder.Services.AddTransient<WebcamDetailVM>();
        builder.Services.AddTransient<WebcamDetailPage>();
        builder.Services.AddTransient<WebcamFilterVM>();
        builder.Services.AddTransient<WebcamFilterPage>();

        return builder.Build();
	}
}
