namespace NationalParks.Views;

public partial class TourImageListPage : ContentPage
{
	public TourImageListPage(TourImageListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}