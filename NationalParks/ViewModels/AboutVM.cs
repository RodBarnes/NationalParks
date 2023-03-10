namespace NationalParks.ViewModels
{
    public partial class AboutVM : BaseVM
    {
        [ObservableProperty] string npsUrl;
        [ObservableProperty] string name;
        [ObservableProperty] string versionString;
        [ObservableProperty] string package;
        [ObservableProperty] string buildString;
        [ObservableProperty] Version version;

        public AboutVM()
        {
            Title = "About";
            NpsUrl = "https://www.nps.gov/subjects/developer/";
            VersionString = AppInfo.Current.VersionString;
            Package = AppInfo.Current.PackageName;
            BuildString = AppInfo.Current.BuildString;
            Name = AppInfo.Name;
        }

        [RelayCommand]
        public void GoToAppInfo()
        {
            AppInfo.ShowSettingsUI();
        }

        [RelayCommand]
        public async Task GoToDataTester()
        {
            await Shell.Current.GoToAsync($"DataTesterPage", true);
        }
    }
}
