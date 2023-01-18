using NationalParks.Services;
using System.Text.Json;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class EventListVM : ListVM
{
    public EventListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        IsBusy = false;
        BaseTitle = "Events";
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    [RelayCommand]
    async Task GetItems()
    {
        if (IsBusy)
            return;

        // Populate the list
        Result result = await GetItems(ResultEvents.Term);
        ResultEvents resultEvents = (ResultEvents)result;
        foreach (var item in resultEvents.Data)
            Items.Add(item);
        StartItems += resultEvents.Data.Count;
        IsPopulated = true;
    }
}
