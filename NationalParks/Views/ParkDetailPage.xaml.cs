namespace NationalParks.Views;

public partial class ParkDetailPage : ContentPage
{
	public ParkDetailPage(ParkDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}