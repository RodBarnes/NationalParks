﻿using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class ParkListVM : ListVM
{
    readonly IConnectivity connectivity;

    public ObservableCollection<Models.Park> Parks { get; } = new();

    public ParkListVM(IConnectivity connectivity, IGeolocation geolocation) : base(geolocation)
    {
        IsBusy = false;
        BaseTitle = "Parks";
        this.connectivity = connectivity;
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    public void ClearData()
    {
        Parks.Clear();
        IsPopulated = false;
    }

    [RelayCommand]
    async Task GetItems()
    {
        if (IsBusy)
            return;

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            IsBusy = true;

            GetFilterSelections();

            // Populate the list
            ResultParks result = await GetParksData(StartItems, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter);

            StartItems += result.Data.Count;
            TotalItems = result.Total;
            IsPopulated = true;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    static async Task GetAlerts(Park park)
    {
        if (park.Alerts?.Count > 0)
            return;

        try
        {
            int startAlerts = 0;
            int totalAlerts = 1;
            int limitAlerts = 20;

            while (totalAlerts > startAlerts)
            {
                var result = await DataService.GetAlertsForParkCodeAsync(park.ParkCode, startAlerts, limitAlerts);
                totalAlerts = result.Total;
                startAlerts += result.Data.Count;
                foreach (var alert in result.Data)
                    park.Alerts.Add(alert);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
    }

    private async Task<ResultParks> GetParksData(int startItems, int limitItems, string statesFilter, string topicsFilter, string activitiesFilter)
    {
        ResultParks result = await DataService.GetParksAsync(startItems, limitItems, topicsFilter, activitiesFilter, statesFilter);
        foreach (var park in result.Data)
        {
            Parks.Add(park);
            await GetAlerts(park);
        }

        return result;
    }

}
