namespace NationalParks.Pages;

public partial class PlaceDetailPage : ContentPage
{
	public PlaceDetailPage(PlaceDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}