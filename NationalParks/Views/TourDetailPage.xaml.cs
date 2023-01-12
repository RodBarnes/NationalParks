namespace NationalParks.Views;

public partial class TourDetailPage : ContentPage
{
    readonly TourDetailVM _vm;

	public TourDetailPage(TourDetailVM vm)
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