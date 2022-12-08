namespace NationalParks;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ParkPage), typeof(ParkPage));
        Routing.RegisterRoute(nameof(FilterPage), typeof(FilterPage));
    }
}