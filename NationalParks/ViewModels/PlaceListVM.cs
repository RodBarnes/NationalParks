using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class PlaceListVM : ListVM
{
    public PlaceListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Places";
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    [RelayCommand]
    async Task GetItems()
    {
        Result result = await GetItems(ResultPlaces.Term);
        ResultPlaces resultPlaces = (ResultPlaces)result;
        foreach (var item in resultPlaces.Data)
            Items.Add(item);
        StartItems += resultPlaces.Data.Count;
        IsPopulated = true;
    }
}
