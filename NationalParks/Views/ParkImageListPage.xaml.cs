namespace NationalParks.Views;

public partial class ParkImageListPage : ContentPage
{
	public ParkImageListPage(ParkImageListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}