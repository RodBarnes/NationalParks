namespace NationalParks.Views;

public partial class WebcamFilterPage : ContentPage
{
	public WebcamFilterPage(WebcamListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}