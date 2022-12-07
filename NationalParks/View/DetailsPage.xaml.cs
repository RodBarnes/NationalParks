namespace NationalParks;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}