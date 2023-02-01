namespace NationalParks.Views;

public partial class PersonFilterPage : ContentPage
{
	public PersonFilterPage(PersonListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}