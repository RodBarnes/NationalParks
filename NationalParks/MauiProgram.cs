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
			})
        	.Services.AddTransient<IGetDeviceInfo, GetDeviceInfo>();

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
        builder.Services.AddSingleton<LogDetailVM>();
        builder.Services.AddSingleton<LogDetailPage>();
        builder.Services.AddSingleton<DataTesterVM>();
        builder.Services.AddSingleton<DataTesterPage>();
        builder.Services.AddSingleton<TestVM>();
        builder.Services.AddSingleton<TestPage>();

        //builder.Services.AddSingleton<ListVM>();

        builder.Services.AddSingleton<ImageListVM>();
        builder.Services.AddSingleton<ImageListPage>();

        builder.Services.AddSingleton<CampgroundListVM>();
        builder.Services.AddSingleton<CampgroundListPage>();
        builder.Services.AddTransient<CampgroundFilterPage>();
        builder.Services.AddTransient<CampgroundDetailVM>();
        builder.Services.AddTransient<CampgroundDetailPage>();

        //builder.Services.AddSingleton<EventListVM>();
        //builder.Services.AddSingleton<EventListPage>();
        //builder.Services.AddTransient<EventFilterPage>();
        //builder.Services.AddTransient<EventDetailVM>();
        //builder.Services.AddTransient<EventDetailPage>();

        builder.Services.AddSingleton<NewsReleaseListVM>();
        builder.Services.AddSingleton<NewsReleaseListPage>();
        builder.Services.AddTransient<NewsReleaseFilterPage>();
        builder.Services.AddTransient<NewsReleaseDetailVM>();
        builder.Services.AddTransient<NewsReleaseDetailPage>();

        builder.Services.AddSingleton<ParkListVM>();
		builder.Services.AddSingleton<ParkListPage>();
        builder.Services.AddTransient<ParkFilterPage>();
        builder.Services.AddTransient<ParkDetailVM>();
		builder.Services.AddTransient<ParkDetailPage>();

        builder.Services.AddSingleton<PersonListVM>();
        builder.Services.AddSingleton<PersonListPage>();
        builder.Services.AddTransient<PersonFilterPage>();
        builder.Services.AddTransient<PersonDetailVM>();
        builder.Services.AddTransient<PersonDetailPage>();

        builder.Services.AddSingleton<PlaceListVM>();
        builder.Services.AddSingleton<PlaceListPage>();
        builder.Services.AddTransient<PlaceFilterPage>();
        builder.Services.AddTransient<PlaceDetailVM>();
        builder.Services.AddTransient<PlaceDetailPage>();

        builder.Services.AddSingleton<ThingToDoListVM>();
        builder.Services.AddSingleton<ThingToDoListPage>();
        builder.Services.AddTransient<ThingToDoFilterPage>();
        builder.Services.AddTransient<ThingToDoDetailVM>();
        builder.Services.AddTransient<ThingToDoDetailPage>();

        builder.Services.AddSingleton<TourListVM>();
        builder.Services.AddSingleton<TourListPage>();
        builder.Services.AddTransient<TourFilterPage>();
        builder.Services.AddTransient<TourDetailVM>();
        builder.Services.AddTransient<TourDetailPage>();

        builder.Services.AddSingleton<WebcamListVM>();
        builder.Services.AddSingleton<WebcamListPage>();
        builder.Services.AddTransient<WebcamFilterPage>();
        builder.Services.AddTransient<WebcamDetailVM>();
        builder.Services.AddTransient<WebcamDetailPage>();

        builder.Services.AddSingleton<ArticleListVM>();
        builder.Services.AddSingleton<ArticleListPage>();
        builder.Services.AddTransient<ArticleFilterPage>();
        builder.Services.AddTransient<ArticleDetailVM>();
        builder.Services.AddTransient<ArticleDetailPage>();

        builder.Services.AddSingleton<VideoListVM>();
        builder.Services.AddSingleton<VideoListPage>();
        builder.Services.AddTransient<VideoFilterPage>();
        builder.Services.AddSingleton<AudioListVM>();
        builder.Services.AddSingleton<AudioListPage>();
        builder.Services.AddTransient<AudioFilterPage>();
        builder.Services.AddTransient<MultimediaDetailVM>();
        builder.Services.AddTransient<MultimediaDetailPage>();

        return builder.Build();
	}
}
