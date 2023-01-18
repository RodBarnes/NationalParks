using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class TourListVM : ListVM
{
    public TourListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        IsBusy = false;
        BaseTitle = "Tours";
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
        Result result = await GetItems(ResultTours.Term);
        ResultTours resultTours = (ResultTours)result;
        foreach (var item in resultTours.Data)
            Items.Add(item);
        StartItems += resultTours.Data.Count;
        IsPopulated = true;
    }

    //private async Task<ResultTours> GetToursData(int startItems, int limitItems, string statesFilter)
    //{
    //    Park park;
    //    ResultTours result = await DataService.GetToursAsync(startItems, limitItems, statesFilter);
    //    foreach (var tour in result.Data)
    //    {
    //        ResultParks resultPark = await DataService.GetParkForParkCodeAsync(tour.Park.ParkCode);
    //        if (resultPark.Data.Count == 1)
    //        {
    //            park = resultPark.Data[0];
    //            tour.Latitude = park.Latitude;
    //            tour.Longitude = park.Longitude;
    //        }
    //        Items.Add(tour);
    //    }

    //    return result;
    //}
}
