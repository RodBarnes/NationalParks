/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿using NationalParks.Views;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Person), "Model")]
public partial class PersonDetailVM : DetailVM
{
    [ObservableProperty] Person person;
    [ObservableProperty] RelatedParksVM relatedParks;
    [ObservableProperty] CollapsibleViewVM bodyText;
    [ObservableProperty] CollapsibleListVM organizations;
    [ObservableProperty] CollapsibleListVM quickFacts;
    [ObservableProperty] CollapsibleListVM tags;

    public PersonDetailVM(IConnectivity connectivity, IMap map) : base(connectivity, map)
    {
        Title = "Person";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = Person;

        BodyText = new CollapsibleTextVM("Full Description", false, Person.BodyText);

        Organizations = new CollapsibleListVM("Related Organizations", false, Person.RelatedOrganizations.ToList<object>());
        QuickFacts = new CollapsibleListVM("Quick Facts", false, Person.QuickFacts.ToList<object>());
        Tags = new CollapsibleListVM("Tags", false, Person.Tags.ToList<object>());

        RelatedParks = new RelatedParksVM("Related Parks", false, Person.RelatedParks);
    }

}
