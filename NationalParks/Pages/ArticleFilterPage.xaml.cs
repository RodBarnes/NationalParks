namespace NationalParks.Pages;

public partial class ArticleFilterPage : ContentPage
{
	public ArticleFilterPage(ArticleListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}