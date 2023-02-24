namespace NationalParks.Pages;

public partial class ImageListPage : ContentPage
{
	public ImageListPage(ImageListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}