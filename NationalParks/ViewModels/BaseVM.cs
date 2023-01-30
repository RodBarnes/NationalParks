namespace NationalParks.ViewModels;

public partial class BaseVM : ObservableObject
{
    [ObservableProperty] bool isBusy;
    [ObservableProperty] string title;

    public bool IsNotBusy => !IsBusy;
}
