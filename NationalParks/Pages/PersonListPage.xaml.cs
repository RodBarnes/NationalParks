namespace NationalParks.Pages;

public partial class PersonListPage : ContentPage
{
    readonly PersonListVM _vm;

	public PersonListPage(PersonListVM vm)
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