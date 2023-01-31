namespace NationalParks.Views;

public partial class CampgroundFilterPage : ContentPage
{
    readonly CampgroundListVM _vm;

	public CampgroundFilterPage(CampgroundListVM vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}
}