using NationalParks.Services;

namespace NationalParks.ViewModels;

public partial class RelatedParksVM : CollapsibleViewVM
{
    [ObservableProperty] List<RelatedPark> items;

    public RelatedParksVM(string title, bool isOpen, List<RelatedPark> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }

    [RelayCommand]
    async Task GoToParkFromParkCode(string parkCode)
    {
        Park park;

        ResultParks result = await DataService.GetItemsForParkCodeAsync<ResultParks>(ResultParks.Term, parkCode);
        if (result.Data.Count > 0)
        {
            park = result.Data.First();
            park.FillMainImage();
            await Shell.Current.GoToAsync(nameof(ParkDetailPage), true, new Dictionary<string, object>
                {
                    {"Model", park }
                });
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", $"Unable to get park for {parkCode}!", "OK");
        }
    }
}
