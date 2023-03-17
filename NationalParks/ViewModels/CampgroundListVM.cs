using System.Reflection;

namespace NationalParks.ViewModels;

public partial class CampgroundListVM : ListVM
{
    public CampgroundListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Campgrounds";
        Term = ResultCampgrounds.Term;
        FilterName = "Campground";
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
            var msg = Utility.ParseException(ex);
            var codeInfo = new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType);
            await Logger.WriteLogEntry($"{codeInfo.ObjectName}.{codeInfo.MethodName}: {msg}");
        }
    }

    [RelayCommand]
    public new async Task GetClosest()
    {
        if (IsBusy)
            return;

        ProgressBar.IsVisible = true;

        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            LimitItems = 50;
            while (TotalItems > Items.Count && ProgressBar.IsVisible)
            {
                ProgressBar.Position = (double)Items.Count / (double)TotalItems;
                await GetItems();
            }
            LimitItems = 20;
        }

        if (ProgressBar.IsVisible)
        {
            await base.GetClosest();
            ProgressBar.IsVisible = false;
        }

        ProgressBar.IsVisible = false;
    }
}
