/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.NewsRelease), "Model")]
public partial class NewsReleaseDetailVM : DetailVM
{
    [ObservableProperty] NewsRelease newsRelease;
    [ObservableProperty] RelatedParksVM relatedParks;
    [ObservableProperty] CollapsibleListVM organizations;

    public NewsReleaseDetailVM(IConnectivity connectivity, IMap map) : base(connectivity, map)
    {
        Title = "News Release";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = NewsRelease;

        Organizations = new CollapsibleListVM("Related Organizations", false, NewsRelease.RelatedOrganizations.ToList<object>());

        RelatedParks = new RelatedParksVM("Related Parks", false, NewsRelease.RelatedParks);
    }
}
