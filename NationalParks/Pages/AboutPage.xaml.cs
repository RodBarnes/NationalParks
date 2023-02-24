namespace NationalParks.Pages;

public partial class AboutPage : ContentPage
{
	public AboutPage(AboutVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}