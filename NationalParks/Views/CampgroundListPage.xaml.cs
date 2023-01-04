namespace NationalParks.Views;

public partial class CampgroundListPage : ContentPage
{
	CampgroundListVM _vm;

	public CampgroundListPage(CampgroundListVM vm)
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