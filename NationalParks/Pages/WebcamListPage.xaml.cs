namespace NationalParks.Pages;

public partial class WebcamListPage : ContentPage
{
    readonly WebcamListVM _vm;

	public WebcamListPage(WebcamListVM vm)
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