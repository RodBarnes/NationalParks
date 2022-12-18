namespace NationalParks.Views;

public partial class FilterPage : ContentPage
{
	readonly FilterVM _vm;

	public FilterPage(FilterVM vm)
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