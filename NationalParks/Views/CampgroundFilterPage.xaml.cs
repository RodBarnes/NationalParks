namespace NationalParks.Views;

public partial class CampgroundFilterPage : ContentPage
{
    readonly CampgroundFilterVM _vm;

	public CampgroundFilterPage(CampgroundFilterVM vm)
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