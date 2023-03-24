namespace NationalParks.Pages;

public partial class ThingToDoListPage : ContentPage
{
    readonly ThingToDoListVM _vm;

	public ThingToDoListPage(ThingToDoListVM vm)
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