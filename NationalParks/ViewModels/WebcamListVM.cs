using NationalParks.Services;

namespace NationalParks.ViewModels;

public partial class WebcamListVM : ListVM
{
    public WebcamListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        IsBusy = false;
        BaseTitle = "Webcams";
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    [RelayCommand]
    async Task GetItems()
    {
        if (IsBusy)
            return;

        // Populate the list
        Result result = await GetItems(ResultWebcams.Term);
        ResultWebcams resultWebcams = (ResultWebcams)result;
        foreach (var item in resultWebcams.Data)
            Items.Add(item);
        StartItems += resultWebcams.Data.Count;
        IsPopulated = true;
    }
}
