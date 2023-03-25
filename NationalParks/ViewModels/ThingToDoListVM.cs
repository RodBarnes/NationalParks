using System.Reflection;

namespace NationalParks.ViewModels;

public partial class ThingToDoListVM : ListVM
{
    public ThingToDoListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Things To Do";
        Term = ResultThingsToDo.Term;
        FilterName = "ThingToDo";
        AllowFilterStates = true;
    }

    [RelayCommand]
    public async Task GetItems()
    {
        if (IsBusy)
            return;

        try
        {
            ResultThingsToDo result = await GetItems<ResultThingsToDo>(ResultThingsToDo.Term);
            foreach (ThingToDo item in result.Data)
            {
                item.FillMainImage();
                Items.Add(item);
            }
            TotalItems = result.Total;
            IsPopulated = true;
        }
        catch (Exception ex)
        {
            await Utility.HandleException(ex, new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType));
        }
    }

    [RelayCommand]
    public async Task GetClosest()
    {
        await GetClosest(GetItems);
    }

    public async Task PopulateData()
    {
        await PopulateData(GetItems);
    }
}
