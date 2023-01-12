namespace NationalParks.Views;

public partial class ThingToDoDetailPage : ContentPage
{
    readonly ThingToDoDetailVM _vm;

	public ThingToDoDetailPage(ThingToDoDetailVM vm)
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