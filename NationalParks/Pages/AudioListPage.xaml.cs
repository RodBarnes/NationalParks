namespace NationalParks.Pages;

public partial class AudioListPage : ContentPage
{
	public AudioListPage(AudioListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}