namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class ThingToDoListVM : ListVM
{
    public ThingToDoListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Things To Do";
        Term = ResultThingsToDo.Term;
        FilterName = "ThingToDo";
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    [RelayCommand]
    new async Task GetItems()
    {
        Result result = await base.GetItems();
        if (result != null)
        {
            ResultThingsToDo resultThingsToDo = (ResultThingsToDo)result;
            foreach (var item in resultThingsToDo.Data)
                Items.Add(item);
            IsPopulated = true;
        }
    }

    [RelayCommand]
    new async Task GetClosest()
    {
        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            LimitItems = 100;
            IsFindingClosest = true;
            while (TotalItems > Items.Count && IsFindingClosest)
            {
                ProgressClosest = (double)Items.Count / (double)TotalItems;
                await GetItems();
            }
            LimitItems = 20;
        }

        if (IsFindingClosest)
        {
            await base.GetClosest();
        }
    }
}
