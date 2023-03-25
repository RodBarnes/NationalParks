namespace NationalParks.Pages;

public partial class ArticleListPage : ContentPage
{
    readonly ArticleListVM _vm;

	public ArticleListPage(ArticleListVM vm)
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