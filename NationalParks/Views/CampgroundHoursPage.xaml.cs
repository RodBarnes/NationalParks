namespace NationalParks.Views;

public partial class CampgroundHoursPage : ContentPage
{
    CampgroundHoursVM _vm;
    
    public CampgroundHoursPage(CampgroundHoursVM vm)
	{
        InitializeComponent();
		BindingContext = _vm = vm;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.PopulateData();
    }
}