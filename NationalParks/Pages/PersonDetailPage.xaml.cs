namespace NationalParks.Views;

public partial class PersonDetailPage : ContentPage
{
	public PersonDetailPage(PersonDetailVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}