namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Person), "Model")]
public partial class PersonDetailVM : DetailVM
{
    [ObservableProperty] Person person;

    public PersonDetailVM(IMap map) : base(map)
    {
        Title = "Person";
    }
}
