namespace NationalParks.Views;

public partial class ParkFilterPage : ContentPage
{
	readonly ParkFilterVM _vm;

	public ParkFilterPage(ParkFilterVM vm)
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