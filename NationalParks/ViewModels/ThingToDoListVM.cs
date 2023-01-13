﻿using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class ThingToDoListVM : ListVM
{
    readonly IConnectivity connectivity;

    public ObservableCollection<Models.ThingToDo> ThingsToDo { get; } = new();

    public ThingToDoListVM(IConnectivity connectivity, IGeolocation geolocation) : base(geolocation)
    {
        IsBusy = false;
        BaseTitle = "Things To Do";
        this.connectivity = connectivity;
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    public void ClearData()
    {
        ThingsToDo.Clear();
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
            string states = "";
            string topics = "";
            string activities = "";

            if (Filter is not null)
            {
                // Apply any filters prior to getting the items
                topics = GetSelectedTopics(Filter.Topics);
                activities = GetSelectedActivities(Filter.Activities);
                states = GetSelectedStates(Filter.States);
            }

            ResultThingsToDo result = await DataService.GetThingsToDoAsync(startItems, limitItems, states);
            foreach (var thingToDo in result.Data)
                ThingsToDo.Add(thingToDo);
            startItems += result.Data.Count;
            totalItems = result.Total;
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
}
