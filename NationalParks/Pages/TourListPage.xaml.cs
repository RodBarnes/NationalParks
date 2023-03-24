namespace NationalParks.Pages;

public partial class TourListPage : ContentPage
{
    readonly TourListVM _vm;

	public TourListPage(TourListVM vm)
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
