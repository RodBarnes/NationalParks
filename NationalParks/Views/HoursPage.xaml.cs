namespace NationalParks.Views;

public partial class HoursPage : ContentPage
{
	HoursVM _vm;

	public HoursPage(HoursVM vm)
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