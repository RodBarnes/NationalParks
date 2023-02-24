namespace NationalParks.Pages;

public partial class PlaceFilterPage : ContentPage
{
	public PlaceFilterPage(PlaceListVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}
}