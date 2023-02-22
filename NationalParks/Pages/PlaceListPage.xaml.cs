namespace NationalParks.Views;

public partial class PlaceListPage : ContentPage
{
	public PlaceListPage(PlaceListVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}