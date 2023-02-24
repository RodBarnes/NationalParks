namespace NationalParks.Pages;

public partial class PersonDetailPage : ContentPage
{
	public PersonDetailPage(PersonDetailVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}