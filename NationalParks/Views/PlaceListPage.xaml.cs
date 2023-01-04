namespace NationalParks.Views;

public partial class PlaceListPage : ContentPage
{
	PlaceListVM _vm;

	public PlaceListPage(PlaceListVM vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}
}