namespace NationalParks.Pages;

public partial class CampgroundDetailPage : ContentPage
{
    public CampgroundDetailPage(CampgroundDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}