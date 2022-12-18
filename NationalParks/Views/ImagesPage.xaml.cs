namespace NationalParks.Views;

public partial class ImagesPage : ContentPage
{
	public ImagesPage(ImagesVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}