namespace NationalParks.Pages;

public partial class VideoListPage : ContentPage
{
	public VideoListPage(VideoListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}