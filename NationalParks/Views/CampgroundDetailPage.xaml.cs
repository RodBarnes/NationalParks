namespace NationalParks.Views;

public partial class CampgroundDetailPage : ContentPage
{
    public CampgroundDetailPage(CampgroundDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}