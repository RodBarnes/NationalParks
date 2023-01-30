namespace NationalParks.Views;

public partial class ParkFilterPage : ContentPage
{
	readonly ParkListVM _vm;

	public ParkFilterPage(ParkListVM vm)
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