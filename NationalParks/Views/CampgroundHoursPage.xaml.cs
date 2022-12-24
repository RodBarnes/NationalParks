namespace NationalParks.Views;

public partial class CampgroundHoursPage : ContentPage
{
	public CampgroundHoursPage(CampgroundHoursVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}