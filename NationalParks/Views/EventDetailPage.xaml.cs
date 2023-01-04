namespace NationalParks.Views;

public partial class EventDetailPage : ContentPage
{
	public EventDetailPage(EventDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}