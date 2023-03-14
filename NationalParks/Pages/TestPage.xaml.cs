namespace NationalParks.Pages;

public partial class TestPage : ContentPage
{
	public TestPage(TestVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}