namespace NationalParks.Views;

public partial class ThingToDoListPage : ContentPage
{
	public ThingToDoListPage(ThingToDoListVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}