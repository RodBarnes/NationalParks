namespace NationalParks;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ParkDetailPage), typeof(ParkDetailPage));
        Routing.RegisterRoute(nameof(FilterPage), typeof(FilterPage));
        Routing.RegisterRoute(nameof(ParkImageListPage), typeof(ParkImageListPage));
        Routing.RegisterRoute(nameof(ParkHoursPage), typeof(ParkHoursPage));
    }
}