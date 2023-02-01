namespace NationalParks.Views;

public partial class NewsReleaseDetailPage : ContentPage
{
	public NewsReleaseDetailPage(NewsReleaseDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}