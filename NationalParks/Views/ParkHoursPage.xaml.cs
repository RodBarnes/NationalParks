namespace NationalParks.Views;

public partial class ParkHoursPage : ContentPage
{
	ParkHoursVM _vm;

	public ParkHoursPage(ParkHoursVM vm)
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