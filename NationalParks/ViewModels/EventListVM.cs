using NationalParks.Services;
using System.Text.Json;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class EventListVM : ListVM
{
    public EventListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
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
        Result result = await GetItems(ResultEvents.Term);
        if (result != null)
        {
            ResultEvents resultEvents = (ResultEvents)result;
            foreach (var item in resultEvents.Data)
                Items.Add(item);
            IsPopulated = true;
        }
    }

    [RelayCommand]
    async Task GetClosest()
    {
        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            while (TotalItems > Items.Count)
            {
                await GetItems();
            }
        }

        await GetClosest(ResultEvents.Term);
    }
}
