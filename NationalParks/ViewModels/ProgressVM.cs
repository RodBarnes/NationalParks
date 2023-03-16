namespace NationalParks.ViewModels;

public partial class ProgressBarVM : ObservableObject
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
