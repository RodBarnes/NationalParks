using NationalParks.Views;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Person), "Model")]
public partial class PersonDetailVM : DetailVM
{
    [ObservableProperty] Person person;
    [ObservableProperty] RelatedParksVM relatedParksVM;
    [ObservableProperty] CollapsibleViewVM bodyTextVM;
    [ObservableProperty] CollapsibleListVM organizationsVM;
    [ObservableProperty] CollapsibleListVM quickFactsVM;
    [ObservableProperty] CollapsibleListVM tagsVM;

    public PersonDetailVM(IMap map) : base(map)
    {
        Title = "Person";
        BodyTextVM = new CollapsibleViewVM("Full Description", false);
    }

    [RelayCommand]
    public void PopulateData()
    {
        RelatedParksVM.HasContent = Person.HasRelatedParks;

        RelatedParksVM = new RelatedParksVM("Related Parks", false, Person.RelatedParks);
        OrganizationsVM = new CollapsibleListVM("Related Organizations", false, Person.RelatedOrganizations.ToList<object>());
        QuickFactsVM = new CollapsibleListVM("Quick Facts", false, Person.QuickFacts.ToList<object>());
        TagsVM = new CollapsibleListVM("Tags", false, Person.Tags.ToList<object>());
    }

}
