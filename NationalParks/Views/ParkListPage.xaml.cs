namespace NationalParks.Views;

public partial class ParkListPage : ContentPage
{
	public ParkListPage(ParkListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}

