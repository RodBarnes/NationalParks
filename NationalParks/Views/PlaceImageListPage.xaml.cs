namespace NationalParks.Views;

public partial class PlaceImageListPage : ContentPage
{
	public PlaceImageListPage(PlaceImageListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}