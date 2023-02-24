namespace NationalParks.Pages;

public partial class ParkFilterPage : ContentPage
{
	public ParkFilterPage(ParkListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}