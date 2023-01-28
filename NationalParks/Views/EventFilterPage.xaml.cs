namespace NationalParks.Views;

public partial class EventFilterPage : ContentPage
{
    readonly EventFilterVM _vm;

    public EventFilterPage(EventFilterVM vm)
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