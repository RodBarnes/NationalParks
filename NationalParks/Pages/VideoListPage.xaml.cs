namespace NationalParks.Pages;

public partial class VideoListPage : ContentPage
{
    readonly VideoListVM _vm;

	public VideoListPage(VideoListVM vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        _vm.PopulateData();
    }
}