/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Place), "Model")]
public partial class PlaceDetailVM : DetailVM
{
    [ObservableProperty] Place place;
    [ObservableProperty] RelatedParksVM relatedParks;
    [ObservableProperty] CollapsibleViewVM bodyText;
    [ObservableProperty] RelatedMultimediaVM multimedia;
    [ObservableProperty] CollapsibleListVM organizations;
    [ObservableProperty] CollapsibleListVM tags;
    [ObservableProperty] CollapsibleListVM quickFacts;
    [ObservableProperty] CollapsibleListVM amenities;

    public PlaceDetailVM(IConnectivity connectivity, IMap map) : base(connectivity, map)
    {
        Title = "Place";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = Place;

        BodyText = new CollapsibleTextVM("Full Description", false, Place.BodyText);

        Organizations = new CollapsibleListVM("Related Organizations", false, Place.RelatedOrganizations.ToList<object>());
        Tags = new CollapsibleListVM("Tags", false, Place.Tags.ToList<object>());
        QuickFacts = new CollapsibleListVM("Quick Facts", false, Place.QuickFacts.ToList<object>());
        Amenities = new CollapsibleListVM("Amenities", false, Place.Amenities.ToList<object>());

        RelatedParks = new RelatedParksVM("Related Parks", false, Place.RelatedParks);
        Multimedia = new RelatedMultimediaVM("Multimedia", false, Place.Multimedia);
    }
}
