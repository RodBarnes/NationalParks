namespace NationalParks.Views;

public partial class FilterPage : ContentPage
{
	public FilterPage(FilterVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}