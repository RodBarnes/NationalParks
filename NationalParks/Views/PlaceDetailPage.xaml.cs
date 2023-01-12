namespace NationalParks.Views;

public partial class PlaceDetailPage : ContentPage
{
	readonly PlaceDetailVM _vm;

	public PlaceDetailPage(PlaceDetailVM vm)
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