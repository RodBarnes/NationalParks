namespace NationalParks.Views;

public partial class ThingToDoFilterPage : ContentPage
{
	readonly ThingToDoListVM _vm;

	public ThingToDoFilterPage(ThingToDoListVM vm)
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