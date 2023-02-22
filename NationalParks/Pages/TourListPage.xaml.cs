namespace NationalParks.Views;

public partial class TourListPage : ContentPage
{
	public TourListPage(TourListVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
