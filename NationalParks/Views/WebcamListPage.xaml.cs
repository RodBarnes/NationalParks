namespace NationalParks.Views;

public partial class WebcamListPage : ContentPage
{
    WebcamListVM _vm;

	public WebcamListPage(WebcamListVM vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!_vm.IsPopulated)
        {
            _vm.PopulateData();
        }
    }
}