namespace NationalParks.Views;

public partial class ThingToDoListPage : ContentPage
{
	readonly ThingToDoListVM _vm;

	public ThingToDoListPage(ThingToDoListVM vm)
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