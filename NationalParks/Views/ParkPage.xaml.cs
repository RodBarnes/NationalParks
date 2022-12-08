namespace NationalParks.Views;

public partial class ParkPage : ContentPage
{
	public ParkPage(ParkVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}