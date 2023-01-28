namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class EventListVM : ListVM
{
    public EventListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Events";
        Term = ResultEvents.Term;
        FilterName = "Event";
    }

    [RelayCommand]
    new async Task GetItems()
    {
        Result result = await base.GetItems();
        if (result != null)
        {
            ResultEvents resultEvents = (ResultEvents)result;
            foreach (var item in resultEvents.Data)
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
            LimitItems = 50;
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
