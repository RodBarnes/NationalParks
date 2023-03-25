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
#pragma warning disable 4014
        _vm.PopulateData();
#pragma warning restore 4014
    }
}
