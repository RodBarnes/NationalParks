namespace NationalParks.Pages;

public partial class PersonFilterPage : ContentPage
{
	public PersonFilterPage(PersonListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}