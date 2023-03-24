namespace NationalParks.Pages;

public partial class CampgroundListPage : ContentPage
{
    readonly CampgroundListVM _vm;

	public CampgroundListPage(CampgroundListVM vm)
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