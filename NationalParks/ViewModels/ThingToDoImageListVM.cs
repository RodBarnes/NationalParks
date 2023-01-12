namespace NationalParks.ViewModels;

[QueryProperty(nameof(ThingToDo), "ThingToDo")]
public partial class ThingToDoImageListVM : BaseVM
{
    // Query properties
    [ObservableProperty] ThingToDo thingToDo;

    public ThingToDoImageListVM()
    {
        Title = "Images";
    }

    [RelayCommand]
    async Task GoToImage(Models.Image image)
    {
        await Shell.Current.DisplayAlert($"Image", $"{image.Title}\n{image.Url}", "OK");
    }
}
