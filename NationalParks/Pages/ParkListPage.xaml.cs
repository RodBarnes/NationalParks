namespace NationalParks.Pages;

public partial class ParkListPage : ContentPage
{
	readonly ParkListVM _vm;

	public ParkListPage(ParkListVM vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        _vm.PopulateData();
    }
}
