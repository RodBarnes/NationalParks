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
#pragma warning disable 4014
        _vm.PopulateData();
#pragma warning restore 4014
    }
}