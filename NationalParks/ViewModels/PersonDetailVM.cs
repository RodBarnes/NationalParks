namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Person), "Model")]
public partial class PersonDetailVM : DetailVM
{
    [ObservableProperty] Person person;
    [ObservableProperty] CollapsibleViewVM relatedParksVM;
    [ObservableProperty] CollapsibleViewVM bodyTextVM;
    [ObservableProperty] CollapsibleViewVM organizationsVM;
    [ObservableProperty] CollapsibleViewVM quickFactsVM;
    [ObservableProperty] CollapsibleListVM tagsVM;

    public PersonDetailVM(IMap map) : base(map)
    {
        Title = "Person";
        RelatedParksVM = new CollapsibleViewVM("Related Parks", false);
        BodyTextVM = new CollapsibleViewVM("Full Description", false);
        OrganizationsVM = new CollapsibleViewVM("Related Organizations", false);
        QuickFactsVM = new CollapsibleViewVM("Quick Facts", false);
        TagsVM = new CollapsibleListVM("Tags", false);
    }

    [RelayCommand]
    public void PopulateData()
    {
        TagsVM.HasContent = person.HasTags;
        TagsVM.Items = person.Tags.ToList<object>();
    }

}
