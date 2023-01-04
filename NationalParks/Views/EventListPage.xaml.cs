namespace NationalParks.Views;

public partial class EventListPage : ContentPage
{
    EventListVM _vm;

	public EventListPage(EventListVM vm)
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