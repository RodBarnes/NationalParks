using System.Reflection;

namespace NationalParks.ViewModels;

public partial class ParkListVM : ListVM
{
    public ParkListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Parks";
        Term = ResultParks.Term;
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
            var msg = Utility.ParseException(ex);
            var codeInfo = new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType);
            await Logger.WriteLogEntry($"{codeInfo.ObjectName}.{codeInfo.MethodName}: {msg}");
        }
    }

    [RelayCommand]
    public new async Task GetClosest()
    {
        await Shell.Current.DisplayAlert("Test", $"Items.Count={Items.Count}", "OK");
        return;

        if (IsBusy)
            return;

        ProgressPanel.IsVisible = true;

        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            LimitItems = 50;
            while (TotalItems > Items.Count && ProgressPanel.IsVisible)
            {
                ProgressPanel.Position = (double)Items.Count / (double)TotalItems;
                await GetItems();
            }
            LimitItems = 20;
        }

        if (ProgressPanel.IsVisible)
        {
            await base.GetClosest();
            ProgressPanel.IsVisible = false;
        }

        IsBusy = false;
    }

    public new async Task PopulateData()
    {
        await GetItems();
        await base.PopulateData();
    }
}
