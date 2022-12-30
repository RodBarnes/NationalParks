namespace NationalParks.Views;

public partial class CampgroundDetailPage : ContentPage
{
	CampgroundDetailVM _vm;

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