namespace NationalParks.ViewModels
{
    public partial class AboutVM : BaseVM
    {
        public AboutVM()
        {
            Title = "About";
        }

        [RelayCommand]
        async Task GoToNpsSite()
        {
            await Shell.Current.DisplayAlert($"NPS", $"Go to NPS developer site", "OK");
        }
    }
}
