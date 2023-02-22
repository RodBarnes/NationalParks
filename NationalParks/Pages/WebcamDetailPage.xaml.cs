namespace NationalParks.Views;

public partial class WebcamDetailPage : ContentPage
{
	public WebcamDetailPage(WebcamDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}