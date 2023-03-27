using System.Reflection;

namespace NationalParks.ViewModels;

public partial class ParkListVM : ListVM
{
    public ParkListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Parks";
        FilterName = "Park";
        AllowFilterStates = true;
        AllowFilterActivities = true;
        AllowFilterTopics = true;
    }

    [RelayCommand]
    public async Task GetItems()
    {
        if (IsBusy)
            return;

        try
        {
            ResultParks result = await GetItems<ResultParks>(ResultParks.Term);
            foreach (Park item in result.Data)
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
