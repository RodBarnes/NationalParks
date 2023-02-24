namespace NationalParks.Pages;

public partial class TourFilterPage : ContentPage
{
	public TourFilterPage(TourListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}