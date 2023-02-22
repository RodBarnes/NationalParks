namespace NationalParks.Views;

public partial class NewsReleaseListPage : ContentPage
{
	public NewsReleaseListPage(NewsReleaseListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}