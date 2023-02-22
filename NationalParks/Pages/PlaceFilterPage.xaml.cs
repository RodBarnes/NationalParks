namespace NationalParks.Views;

public partial class PlaceFilterPage : ContentPage
{
	public PlaceFilterPage(PlaceListVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}
}