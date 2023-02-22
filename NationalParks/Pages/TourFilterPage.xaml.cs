namespace NationalParks.Views;

public partial class TourFilterPage : ContentPage
{
	public TourFilterPage(TourListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}