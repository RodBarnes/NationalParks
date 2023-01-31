namespace NationalParks.Views;

public partial class CampgroundFilterPage : ContentPage
{
	public CampgroundFilterPage(CampgroundListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}