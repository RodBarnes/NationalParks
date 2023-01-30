namespace NationalParks.Views;

public partial class EventFilterPage : ContentPage
{
    readonly EventListVM _vm;

    public EventFilterPage(EventListVM vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.PopulateFilterData();
    }
}