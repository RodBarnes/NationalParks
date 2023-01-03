namespace NationalParks.Views;

public partial class ParkDetailPage : ContentPage
{
	ParkDetailVM _vm;

	public ParkDetailPage(ParkDetailVM vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}
}