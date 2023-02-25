namespace NationalParks.Pages;

public partial class VideoFilterPage : ContentPage
{
	public VideoFilterPage(VideoListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}