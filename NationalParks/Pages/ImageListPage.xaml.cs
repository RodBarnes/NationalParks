namespace NationalParks.Views;

public partial class ImageListPage : ContentPage
{
	public ImageListPage(ImageListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}