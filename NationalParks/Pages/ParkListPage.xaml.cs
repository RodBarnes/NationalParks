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
#pragma warning disable 4014
        _vm.PopulateData();
#pragma warning restore 4014
    }
}
