namespace NationalParks.Pages;

public partial class ThingToDoFilterPage : ContentPage
{
	public ThingToDoFilterPage(ThingToDoListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}