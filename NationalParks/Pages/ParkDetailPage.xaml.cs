namespace NationalParks.Pages;

public partial class ParkDetailPage : ContentPage
{
	public ParkDetailPage(ParkDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}