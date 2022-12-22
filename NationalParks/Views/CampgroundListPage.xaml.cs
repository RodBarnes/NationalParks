namespace NationalParks.Views;

public partial class CampgroundListPage : ContentPage
{
	public CampgroundListPage(CampgroundListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}