using System.Reflection;

namespace NationalParks.ViewModels;

public partial class AudioListVM : ListVM
{
    public AudioListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Audios";
        FilterName = "Audio";
        AllowFilterStates = true;
    }

    [RelayCommand]
    public async Task GetItems()
    {
        if (IsBusy)
            return;

        try
        {
            ResultAudios result = await GetItems<ResultAudios>(ResultAudios.Term);
            foreach (Multimedia item in result.Data)
            {
                item.FillMainImage();
                Items.Add(item);
            }
            TotalItems = result.Total;
            IsPopulated = true;
        }
        catch (Exception ex)
        {
            await Utility.HandleException(ex, new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType));
        }
    }

    [RelayCommand]
    public async Task GetClosest()
    {
        await GetClosest(GetItems);
    }

    public async Task PopulateData()
    {
        await PopulateData(GetItems);
    }
}
