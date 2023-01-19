using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class ParkListVM : ListVM
{
    public ParkListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Parks";
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    [RelayCommand]
    async Task GetItems()
    {
        // Populate the list
        Result result = await GetItems(ResultParks.Term);
        ResultParks resultParks = (ResultParks)result;
        foreach (var item in resultParks.Data)
            Items.Add(item);
        StartItems += resultParks.Data.Count;
        IsPopulated = true;
    }
}
