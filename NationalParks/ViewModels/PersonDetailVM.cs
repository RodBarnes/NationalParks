namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Person), "Model")]
public partial class PersonDetailVM : DetailVM
{
    [ObservableProperty] Person person;
    [ObservableProperty] RelatedParksVM relatedParksVM;
    [ObservableProperty] CollapsibleViewVM bodyTextVM;
    [ObservableProperty] CollapsibleViewVM organizationsVM;
    [ObservableProperty] CollapsibleListVM quickFactsVM;
    [ObservableProperty] CollapsibleListVM tagsVM;

    public PersonDetailVM(IMap map) : base(map)
    {
        Title = "Person";
        RelatedParksVM = new RelatedParksVM("Related Parks", false);
        BodyTextVM = new CollapsibleViewVM("Full Description", false);
        OrganizationsVM = new CollapsibleViewVM("Related Organizations", false);
        QuickFactsVM = new CollapsibleListVM("Quick Facts", false);
        TagsVM = new CollapsibleListVM("Tags", false);
    }

    [RelayCommand]
    public void PopulateData()
    {
        RelatedParksVM.HasContent = Person.HasRelatedParks;
        RelatedParksVM.Items = Person.RelatedParks;
        TagsVM.HasContent = Person.HasTags;
        TagsVM.Items = Person.Tags.ToList<object>();
        QuickFactsVM.HasContent = Person.HasQuickFacts;
        QuickFactsVM.Items = Person.QuickFacts.ToList<object>();
    }

}
