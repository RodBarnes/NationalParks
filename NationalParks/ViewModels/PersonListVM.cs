namespace NationalParks.ViewModels;

public partial class PersonListVM : ListVM
{
    public PersonListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "People";
        Term = ResultPeople.Term;
        FilterName = "Person";
        AllowFilterStates = true;
    }
}
