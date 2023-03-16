namespace NationalParks.ViewModels;

public partial class ProgressVM : ObservableObject
{
    [ObservableProperty] double position;
    [ObservableProperty] string text;
    [ObservableProperty] bool isVisible;

    [RelayCommand]
    public void CancelProgress()
    {
        IsVisible = false;
    }

}
