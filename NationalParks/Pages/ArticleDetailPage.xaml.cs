namespace NationalParks.Pages;

public partial class ArticleDetailPage : ContentPage
{
	public ArticleDetailPage(ArticleDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}