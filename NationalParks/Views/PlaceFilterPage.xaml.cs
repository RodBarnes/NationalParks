namespace NationalParks.Views;

public partial class PlaceFilterPage : ContentPage
{
    readonly PlaceListVM _vm;

	public PlaceFilterPage(PlaceListVM vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
	}


    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.PopulateFilterData();
    }

}