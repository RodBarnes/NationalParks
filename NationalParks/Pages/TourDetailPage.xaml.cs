namespace NationalParks.Pages;

public partial class TourDetailPage : ContentPage
{
	public TourDetailPage(TourDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}