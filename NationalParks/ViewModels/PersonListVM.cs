namespace NationalParks.ViewModels;

public partial class PersonListVM : ListVM
{
    public PersonListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "People";
        Term = ResultPeople.Term;
        FilterName = "Person";
        AllowFilterStates = true;
    }

    [RelayCommand]
    public new async Task PopulateData()
    {
        await GetItems();
        await base.PopulateData();
    }

    [RelayCommand]
    public async Task GetItems()
    {
        if (IsBusy)
            return;

        ResultPeople result = await GetItems<ResultPeople>(ResultPeople.Term);
        foreach (Person item in result.Data)
        {
            item.FillMainImage();
            Items.Add(item);
        }
        TotalItems = result.Total;
        IsPopulated = true;
    }

    [RelayCommand]
    public new async Task GetClosest()
    {
        if (IsBusy)
            return;

        ShowClosestProgress = true;

        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            LimitItems = 50;
            while (TotalItems > Items.Count)
            {
                ProgressClosest = (double)Items.Count / (double)TotalItems;
                await GetItems();
            }
            LimitItems = 20;
        }

        await base.GetClosest();

        ShowClosestProgress = false;
    }
}
