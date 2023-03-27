using NationalParks.Models;
using System.Reflection;

namespace NationalParks.ViewModels;

public partial class NewsReleaseListVM : ListVM
{
    public NewsReleaseListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "News Releases";
        FilterName = "NewsRelease";
        AllowFilterStates = true;
    }

    [RelayCommand]
    public async Task GetItems()
    {
        if (IsBusy)
            return;

        try
        {
            ResultNewsReleases result = await GetItems<ResultNewsReleases>(ResultNewsReleases.Term);
            foreach (NewsRelease item in result.Data)
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
