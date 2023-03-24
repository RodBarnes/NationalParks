namespace NationalParks.Pages;

public partial class AudioListPage : ContentPage
{
    readonly AudioListVM _vm;

	public AudioListPage(AudioListVM vm)
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