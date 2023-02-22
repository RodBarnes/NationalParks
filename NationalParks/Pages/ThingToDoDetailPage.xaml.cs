namespace NationalParks.Views;

public partial class ThingToDoDetailPage : ContentPage
{
	public ThingToDoDetailPage(ThingToDoDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}