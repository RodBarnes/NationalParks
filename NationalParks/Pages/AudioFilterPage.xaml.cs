namespace NationalParks.Pages;

public partial class AudioFilterPage : ContentPage
{
	public AudioFilterPage(AudioListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}