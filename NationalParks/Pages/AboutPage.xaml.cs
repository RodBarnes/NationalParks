namespace NationalParks.Views;

public partial class AboutPage : ContentPage
{
	public AboutPage(AboutVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}