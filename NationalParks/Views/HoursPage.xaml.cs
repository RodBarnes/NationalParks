namespace NationalParks.Views;

public partial class HoursPage : ContentPage
{
	public HoursPage(HoursVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}