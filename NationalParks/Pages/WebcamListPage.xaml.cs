namespace NationalParks.Pages;

public partial class WebcamListPage : ContentPage
{
	public WebcamListPage(WebcamListVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}