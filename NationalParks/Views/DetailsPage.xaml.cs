namespace NationalParks.Views;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}