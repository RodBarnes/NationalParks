namespace NationalParks.Pages;

public partial class WebcamFilterPage : ContentPage
{
	public WebcamFilterPage(WebcamListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}