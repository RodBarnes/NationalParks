namespace NationalParks.Pages;

public partial class LogDetailPage : ContentPage
{
	public LogDetailPage(LogDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}