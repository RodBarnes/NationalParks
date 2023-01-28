namespace NationalParks.Views;

public partial class WebcamFilterPage : ContentPage
{
	readonly WebcamFilterVM _vm;

	public WebcamFilterPage(WebcamFilterVM vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.PopulateData();
    }
}