namespace NationalParks;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));

        Routing.RegisterRoute(nameof(DataTesterPage), typeof(DataTesterPage));

        Routing.RegisterRoute(nameof(ImageListPage), typeof(ImageListPage));

        Routing.RegisterRoute(nameof(ParkDetailPage), typeof(ParkDetailPage));
        Routing.RegisterRoute(nameof(ParkFilterPage), typeof(ParkFilterPage));

        Routing.RegisterRoute(nameof(CampgroundDetailPage), typeof(CampgroundDetailPage));
        Routing.RegisterRoute(nameof(CampgroundFilterPage), typeof(CampgroundFilterPage));

        Routing.RegisterRoute(nameof(TourDetailPage), typeof(TourDetailPage));
        Routing.RegisterRoute(nameof(TourFilterPage), typeof(TourFilterPage));

        Routing.RegisterRoute(nameof(PlaceDetailPage), typeof(PlaceDetailPage));
        Routing.RegisterRoute(nameof(PlaceFilterPage), typeof(PlaceFilterPage));

        Routing.RegisterRoute(nameof(ThingToDoDetailPage), typeof(ThingToDoDetailPage));
        Routing.RegisterRoute(nameof(ThingToDoFilterPage), typeof(ThingToDoFilterPage));

        Routing.RegisterRoute(nameof(EventDetailPage), typeof(EventDetailPage));

        Routing.RegisterRoute(nameof(WebcamDetailPage), typeof(WebcamDetailPage));
    }
}