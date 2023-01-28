namespace NationalParks.Views;

public partial class ThingToDoFilterPage : ContentPage
{
	readonly ThingToDoFilterVM _vm;

	public ThingToDoFilterPage(ThingToDoFilterVM vm)
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