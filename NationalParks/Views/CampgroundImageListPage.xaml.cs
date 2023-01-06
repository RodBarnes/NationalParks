namespace NationalParks.Views;

public partial class CampgroundImageListPage : ContentPage
{
	public CampgroundImageListPage(CampgroundImageListVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}