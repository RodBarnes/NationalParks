namespace NationalParks.Pages;

public partial class PersonListPage : ContentPage
{
	public PersonListPage(PersonListVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}