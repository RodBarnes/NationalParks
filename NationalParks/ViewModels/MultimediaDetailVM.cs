﻿namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Multimedia), "Model")]
public partial class MultimediaDetailVM : DetailVM
{
    [ObservableProperty] Multimedia multimedia;
    [ObservableProperty] RelatedParksVM relatedParksVM;
    [ObservableProperty] CollapsibleListVM tagsVM;

    public MultimediaDetailVM(IMap map) : base(map)
    {
        Title = "Multimedia";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = Multimedia;

        TagsVM = new CollapsibleListVM("Tags", false, Multimedia.Tags.ToList<object>());

        RelatedParksVM = new RelatedParksVM("Related Parks", false, Multimedia.RelatedParks);
    }
}
