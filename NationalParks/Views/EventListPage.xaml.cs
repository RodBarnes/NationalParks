namespace NationalParks.Views;

public partial class EventListPage : ContentPage
{
	public EventListPage(EventListVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}