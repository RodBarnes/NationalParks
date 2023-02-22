namespace NationalParks.Views;

public partial class NewsReleaseFilterPage : ContentPage
{
	public NewsReleaseFilterPage(NewsReleaseListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}