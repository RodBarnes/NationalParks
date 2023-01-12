namespace NationalParks.Views;

public partial class ThingToDoImageListPage : ContentPage
{
	public ThingToDoImageListPage(ThingToDoImageListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}