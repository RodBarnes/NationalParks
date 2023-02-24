namespace NationalParks.Pages;

public partial class NewsReleaseFilterPage : ContentPage
{
	public NewsReleaseFilterPage(NewsReleaseListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}