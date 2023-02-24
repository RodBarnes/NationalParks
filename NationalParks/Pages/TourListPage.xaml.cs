namespace NationalParks.Pages;

public partial class TourListPage : ContentPage
{
	public TourListPage(TourListVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
