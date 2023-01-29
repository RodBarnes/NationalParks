namespace NationalParks.Views;

public partial class ListContentPage : ContentPage
{
    readonly ListVM _vm;

    public ListContentPage(ListVM vm)
    {
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
