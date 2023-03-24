namespace NationalParks.Pages;

public partial class PersonListPage : ContentPage
{
    readonly PersonListVM _vm;

	public PersonListPage(PersonListVM vm)
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