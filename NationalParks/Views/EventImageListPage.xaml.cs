namespace NationalParks.Views;

public partial class EventImageListPage : ContentPage
{
	public EventImageListPage(EventImageListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}