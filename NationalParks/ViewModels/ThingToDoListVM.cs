namespace NationalParks.ViewModels;

public partial class ThingToDoListVM : ListVM
{
    public ThingToDoListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Things To Do";
        Term = ResultThingsToDo.Term;
        FilterName = "ThingToDo";
    }
}
