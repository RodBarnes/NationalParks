namespace NationalParks.Pages;

public partial class ParkListPage : ContentPage
{
	public ParkListPage(ParkListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
