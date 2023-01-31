namespace NationalParks.Views;

public partial class ParkFilterPage : ContentPage
{
	public ParkFilterPage(ParkListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}