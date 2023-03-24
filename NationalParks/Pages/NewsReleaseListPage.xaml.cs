namespace NationalParks.Pages;

public partial class NewsReleaseListPage : ContentPage
{
    readonly NewsReleaseListVM _vm;

	public NewsReleaseListPage(NewsReleaseListVM vm)
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