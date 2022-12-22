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
        Routing.RegisterRoute(nameof(ParkHoursPage), typeof(ParkHoursPage));
    }
}