namespace NationalParks.ViewModels;

//[QueryProperty(nameof(Images), "Images")]
public partial class TestVM : BaseVM
{
    // Query properties
    //[ObservableProperty] List<Models.Image> images;

    public TestVM()
    {
        Title = "Test";
    }
}
