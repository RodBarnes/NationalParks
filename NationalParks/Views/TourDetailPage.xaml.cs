namespace NationalParks.Views;

public partial class TourDetailPage : ContentPage
{
	public TourDetailPage(TourDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}