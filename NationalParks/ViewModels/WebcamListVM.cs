using System.Reflection;

namespace NationalParks.ViewModels;

public partial class WebcamListVM : ListVM
{
    public WebcamListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Webcams";
        Term = ResultWebcams.Term;
        FilterName = "Webcam";
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
            ResultWebcams result = await GetItems<ResultWebcams>(ResultWebcams.Term);
            foreach (Webcam item in result.Data)
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
