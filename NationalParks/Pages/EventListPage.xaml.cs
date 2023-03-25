namespace NationalParks.Pages;

public partial class EventListPage : ContentPage
{
    readonly EventListVM _vm;

	public EventListPage(EventListVM vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
#pragma warning disable 4014
        _vm.PopulateData();
#pragma warning restore 4014
    }
}