using NationalParks.Services;
using System.Reflection;

namespace NationalParks.ViewModels;

public partial class TourListVM : ListVM
{
    public TourListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Tours";
        Term = ResultTours.Term;
        FilterName = "Tour";
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
            ResultTours result = await GetItems<ResultTours>(ResultTours.Term);
            foreach (Tour item in result.Data)
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
            await Shell.Current.DisplayAlert("Error!", $"{codeInfo.ObjectName}.{codeInfo.MethodName}: {msg}", "OK");
        }
    }

    [RelayCommand]
    public new async Task GetClosest()
    {
        if (IsBusy)
            return;

        Progress.IsVisible = true;

        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            LimitItems = 50;
            while (TotalItems > Items.Count && Progress.IsVisible)
            {
                Progress.Position = (double)Items.Count / (double)TotalItems;
                await GetItems();
            }
            LimitItems = 20;
        }

        if (Progress.IsVisible)
        {
            await base.GetClosest();
            Progress.IsVisible = false;
        }

        Progress.IsVisible = false;
    }
}
