namespace NationalParks.View;

public partial class MainPage : ContentPage
{
	public MainPage(MainVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

