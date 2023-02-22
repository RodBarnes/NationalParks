namespace NationalParks.Views;

public partial class PersonListPage : ContentPage
{
	public PersonListPage(PersonListVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}