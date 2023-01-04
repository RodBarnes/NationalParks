namespace NationalParks;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));

        Routing.RegisterRoute(nameof(ParkDetailPage), typeof(ParkDetailPage));
        Routing.RegisterRoute(nameof(ParkFilterPage), typeof(ParkFilterPage));
        Routing.RegisterRoute(nameof(ParkImageListPage), typeof(ParkImageListPage));

        Routing.RegisterRoute(nameof(CampgroundDetailPage), typeof(CampgroundDetailPage));
        Routing.RegisterRoute(nameof(CampgroundFilterPage), typeof(CampgroundFilterPage));
        Routing.RegisterRoute(nameof(CampgroundImageListPage), typeof(CampgroundImageListPage));

        Routing.RegisterRoute(nameof(TourDetailPage), typeof(TourDetailPage));

        Routing.RegisterRoute(nameof(WebcamDetailPage), typeof(WebcamDetailPage));
    }
}