namespace NationalParks.Views;

public partial class EventFilterPage : ContentPage
{
    public EventFilterPage(EventListVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}