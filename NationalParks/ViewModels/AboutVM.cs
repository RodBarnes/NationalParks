using CommunityToolkit.Maui.Alerts;

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

        [RelayCommand]
        public async Task ReadAllLog()
        {
            try
            {
                var content = await Logger.ReadLog();
                await Shell.Current.DisplayAlert("Log", content, "OK");
            }
            catch (Exception ex)
            {
                await Toast.Make(ex.Message).Show();
            }
        }

        [RelayCommand]
        public void DeleteLog()
        {
            Logger.DeleteLog();
        }
    }
}
