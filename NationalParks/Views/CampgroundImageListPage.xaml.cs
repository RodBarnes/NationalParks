namespace NationalParks.Views;

public partial class CampgroundImageListPage : ContentPage
{
	CampgroundImageListVM _vm;

	public CampgroundImageListPage(CampgroundImageListVM vm)
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