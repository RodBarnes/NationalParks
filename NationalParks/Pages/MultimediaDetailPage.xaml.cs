namespace NationalParks.Pages;

public partial class MultimediaDetailPage : ContentPage
{
	public MultimediaDetailPage(MultimediaDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}