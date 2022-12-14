namespace NationalParks.Views;

public partial class PlaceFilterPage : ContentPage
{
    PlaceFilterVM _vm;

	public PlaceFilterPage(PlaceFilterVM vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
	}


    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.PopulateData();
    }

}