namespace NationalParks.Views;

public partial class TourListPage : ContentPage
{
    TourListVM _vm;

	public TourListPage(TourListVM vm)
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

