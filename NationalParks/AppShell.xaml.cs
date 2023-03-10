namespace NationalParks;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));

        Routing.RegisterRoute(nameof(ImageListPage), typeof(ImageListPage));

        Routing.RegisterRoute(nameof(DataTesterPage), typeof(DataTesterPage));

        Routing.RegisterRoute(nameof(CampgroundDetailPage), typeof(CampgroundDetailPage));
        Routing.RegisterRoute(nameof(CampgroundFilterPage), typeof(CampgroundFilterPage));

        //Routing.RegisterRoute(nameof(EventDetailPage), typeof(EventDetailPage));
        //Routing.RegisterRoute(nameof(EventFilterPage), typeof(EventFilterPage));

        Routing.RegisterRoute(nameof(NewsReleaseDetailPage), typeof(NewsReleaseDetailPage));
        Routing.RegisterRoute(nameof(NewsReleaseFilterPage), typeof(NewsReleaseFilterPage));

        Routing.RegisterRoute(nameof(ParkDetailPage), typeof(ParkDetailPage));
        Routing.RegisterRoute(nameof(ParkFilterPage), typeof(ParkFilterPage));

        Routing.RegisterRoute(nameof(PersonDetailPage), typeof(PersonDetailPage));
        Routing.RegisterRoute(nameof(PersonFilterPage), typeof(PersonFilterPage));

        Routing.RegisterRoute(nameof(PlaceDetailPage), typeof(PlaceDetailPage));
        Routing.RegisterRoute(nameof(PlaceFilterPage), typeof(PlaceFilterPage));

        Routing.RegisterRoute(nameof(ThingToDoDetailPage), typeof(ThingToDoDetailPage));
        Routing.RegisterRoute(nameof(ThingToDoFilterPage), typeof(ThingToDoFilterPage));

        Routing.RegisterRoute(nameof(TourDetailPage), typeof(TourDetailPage));
        Routing.RegisterRoute(nameof(TourFilterPage), typeof(TourFilterPage));

        Routing.RegisterRoute(nameof(WebcamDetailPage), typeof(WebcamDetailPage));
        Routing.RegisterRoute(nameof(WebcamFilterPage), typeof(WebcamFilterPage));

        Routing.RegisterRoute(nameof(ArticleDetailPage), typeof(ArticleDetailPage));
        Routing.RegisterRoute(nameof(ArticleFilterPage), typeof(ArticleFilterPage));

        Routing.RegisterRoute(nameof(MultimediaDetailPage), typeof(MultimediaDetailPage));
        Routing.RegisterRoute(nameof(VideoFilterPage), typeof(VideoFilterPage));
        Routing.RegisterRoute(nameof(AudioFilterPage), typeof(AudioFilterPage));
    }
}