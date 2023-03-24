namespace NationalParks.Pages;

public partial class PlaceListPage : ContentPage
{
    readonly PlaceListVM _vm;

	public PlaceListPage(PlaceListVM vm)
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