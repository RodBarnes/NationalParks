namespace NationalParks.Views;

public partial class WebcamFilterPage : ContentPage
{
	readonly WebcamListVM _vm;

	public WebcamFilterPage(WebcamListVM vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}
}