﻿using NationalParks.Services;

namespace NationalParks.ViewModels;

public partial class WebcamListVM : ListVM
{
    readonly IConnectivity connectivity;

    public ObservableCollection<Models.Webcam> Webcams { get; } = new();

    public WebcamListVM(IConnectivity connectivity, IGeolocation geolocation) : base(geolocation)
    {
        BaseTitle = "Webcams";
        this.connectivity = connectivity;
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

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            IsBusy = true;

            // Populate the list
            ResultWebcams result = await GetWebcamsData(StartItems, LimitItems);

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

    private async Task<ResultWebcams> GetWebcamsData(int startItems, int limitItems)
    {
        ResultWebcams result = await DataService.GetWebcamsAsync(startItems, limitItems);
        foreach (var webcam in result.Data)
            Webcams.Add(webcam);

        return result;
    }
}
