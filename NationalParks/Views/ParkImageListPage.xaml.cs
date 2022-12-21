namespace NationalParks.Views;

public partial class ParkImageListPage : ContentPage
{
    ParkImageListVM _vm;

	public ParkImageListPage(ParkImageListVM vm)
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