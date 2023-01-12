namespace NationalParks.Views;

public partial class CampgroundDetailPage : ContentPage
{
	readonly CampgroundDetailVM _vm;

    public CampgroundDetailPage(CampgroundDetailVM vm)
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