namespace NationalParks.Pages;

public partial class ThingToDoDetailPage : ContentPage
{
	public ThingToDoDetailPage(ThingToDoDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}