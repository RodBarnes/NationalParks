namespace NationalParks.Views;

public partial class WebcamDetailPage : ContentPage
{
	readonly WebcamDetailVM _vm;
	public WebcamDetailPage(WebcamDetailVM vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_vm.PopulateData();
    }
}