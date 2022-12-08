using NationalParks.Views;

namespace NationalParks;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ParkPage), typeof(ParkPage));
	}
}