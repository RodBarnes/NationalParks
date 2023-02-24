namespace NationalParks.Pages;

public partial class ArticleListPage : ContentPage
{
	public ArticleListPage(ArticleListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}