namespace NationalParks.Views;

public partial class TourFilterPage : ContentPage
{
	readonly TourFilterVM _vm;

	public TourFilterPage(TourFilterVM vm)
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