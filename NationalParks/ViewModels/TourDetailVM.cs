using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Tour), "Model")]
public partial class TourDetailVM : DetailVM
{
    [ObservableProperty] Tour tour;
    [ObservableProperty] AvatarVM avatarVM;
    [ObservableProperty] CollapsibleListVM stopsVM;
    [ObservableProperty] CollapsibleListVM tagsVM;
    [ObservableProperty] CollapsibleListVM topicsVM;
    [ObservableProperty] CollapsibleListVM activitiesVM;

    public TourDetailVM(IMap map) : base(map)
    {
        Title = "Tour";
    }

    [RelayCommand]
    public void PopulateData()
    {
        AvatarVM = new AvatarVM(Tour.MainImage);

        StopsVM = new CollapsibleListVM("Stops", false, Tour.Stops.ToList<object>());
        TagsVM = new CollapsibleListVM("Tags", false, Tour.Tags.ToList<object>());
        TopicsVM = new CollapsibleListVM("Topics", false, Tour.Topics.ToList<object>());
        ActivitiesVM = new CollapsibleListVM("Activities", false, Tour.Activities.ToList<object>());
    }
}
