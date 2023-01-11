namespace NationalParks.Views;

public partial class ParkListPage : ContentPage
{
    readonly ParkListVM _vm;

	public ParkListPage(ParkListVM vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!_vm.IsPopulated)
        {
            _vm.PopulateData();
        }
    }
}
