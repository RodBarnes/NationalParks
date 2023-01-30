namespace NationalParks.Views;

public partial class TourFilterPage : ContentPage
{
	readonly TourListVM _vm;

	public TourFilterPage(TourListVM vm)
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