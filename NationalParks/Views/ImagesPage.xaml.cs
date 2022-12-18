namespace NationalParks.Views;

public partial class ImagesPage : ContentPage
{
    ImagesVM _vm;

	public ImagesPage(ImagesVM vm)
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