using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class ThingToDoListVM : ListVM
{
    public ThingToDoListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Things To Do";
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    [RelayCommand]
    async Task GetItems()
    {
        Result result = await GetItems(ResultThingsToDo.Term);
        if (result != null)
        {
            ResultThingsToDo resultThingsToDo = (ResultThingsToDo)result;
            foreach (var item in resultThingsToDo.Data)
                Items.Add(item);
            StartItems += resultThingsToDo.Data.Count;
            IsPopulated = true;
        }
    }

    [RelayCommand]
    async Task GetClosest()
    {
        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            while (TotalItems > StartItems)
            {
                await GetItems();
            }
        }

        await GetClosest(ResultThingsToDo.Term);
    }
}
