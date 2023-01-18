﻿using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class CampgroundListVM : ListVM
{
    public CampgroundListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        IsBusy = false;
        BaseTitle = "Campgrounds";
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
        Result result = await GetItems(ResultCampgrounds.Term);
        ResultCampgrounds resultCampgrounds = (ResultCampgrounds)result;
        foreach (var item in resultCampgrounds.Data)
            Items.Add(item);
        StartItems += resultCampgrounds.Data.Count;
        IsPopulated = true;
    }
}
