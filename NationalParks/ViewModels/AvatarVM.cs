namespace NationalParks.ViewModels;

public partial class AvatarVM : BaseVM
{
    [ObservableProperty] ImageSource image;

    public AvatarVM(ImageSource image)
    {
        Image = image;
    }
}
