using System.Reflection;

namespace NationalParks.ViewModels;

public partial class CampgroundListVM : ListVM
{
    public CampgroundListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Campgrounds";
        FilterName = "Campground";
        AllowFilterStates = true;
    }

    [RelayCommand]
    public async Task GetItems()
    {
        if (IsBusy)
            return;

        try
        {
            ResultCampgrounds result = await GetItems<ResultCampgrounds>(ResultCampgrounds.Term);
            foreach (Campground item in result.Data)
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
