using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class ThingToDoListVM : ListVM
{
    public ThingToDoListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        IsBusy = false;
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
        if (IsBusy)
            return;

        // Populate the list
        Result result = await GetItems(ResultThingsToDo.Term);
        ResultThingsToDo resultThingsToDo = (ResultThingsToDo)result;
        foreach (var item in resultThingsToDo.Data)
            Items.Add(item);
        StartItems += resultThingsToDo.Data.Count;
        IsPopulated = true;
    }
}
