namespace NationalParks.Pages;

public partial class VideoListPage : ContentPage
{
    readonly VideoListVM _vm;

	public VideoListPage(VideoListVM vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!_vm.IsPopulated)
        {
#pragma warning disable 4014
            _vm.PopulateData();
#pragma warning restore 4014
        }
    }
}