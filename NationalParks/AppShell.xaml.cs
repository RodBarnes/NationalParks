namespace NationalParks;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ParkDetailPage), typeof(ParkDetailPage));
        Routing.RegisterRoute(nameof(FilterPage), typeof(FilterPage));
        Routing.RegisterRoute(nameof(ImagesPage), typeof(ImagesPage));
        Routing.RegisterRoute(nameof(HoursPage), typeof(HoursPage));
    }
}