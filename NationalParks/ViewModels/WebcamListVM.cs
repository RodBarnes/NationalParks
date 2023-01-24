namespace NationalParks.ViewModels;

public partial class WebcamListVM : ListVM
{
    public WebcamListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Webcams";
        Term = ResultWebcams.Term;
        FilterName = "Webcam";
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    [RelayCommand]
    new async Task GetItems()
    {
        Result result = await base.GetItems();
        if (result != null)
        {
            ResultWebcams resultWebcams = (ResultWebcams)result;
            foreach (var item in resultWebcams.Data)
                Items.Add(item);
            IsPopulated = true;
        }
    }

    [RelayCommand]
    new async Task GetClosest()
    {
        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            IsFindingClosest = true;
            while (TotalItems > Items.Count)
            {
                ProgressClosest = (double)Items.Count / (double)TotalItems;
                await GetItems();
            }
            IsFindingClosest = false;
        }

        await base.GetClosest();
    }
}
