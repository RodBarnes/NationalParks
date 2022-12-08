namespace NationalParks.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}