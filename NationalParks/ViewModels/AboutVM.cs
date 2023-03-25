using System.Reflection;

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
        [ObservableProperty] string supportUrl;

        public AboutVM()
        {
            Title = "About";
            NpsUrl = "https://www.nps.gov/subjects/developer/";
            VersionString = AppInfo.Current.VersionString;
            Package = AppInfo.Current.PackageName;
            BuildString = AppInfo.Current.BuildString;
            Name = AppInfo.Name;
            SupportUrl = $"mailto:{Config.SupportEmailAddress}";

        }

        [RelayCommand]
        public async Task ComposeSupportRequest()
        {
            try
            {
                await Logger.WriteLogEntry("*** Support Request ***");
                await Utility.SupportMessage("Support Request", true);
            }
            catch (Exception ex)
            {
                await Utility.HandleException(ex, new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType));
            }
        }

        [RelayCommand]
        public void GoToAppInfo()
        {
            AppInfo.ShowSettingsUI();
        }

        [RelayCommand]
        public async Task GoToDataTester()
        {
            try
            {
                await Shell.Current.GoToAsync($"DataTesterPage", true);
            }
            catch (Exception ex)
            {
                await Utility.HandleException(ex, new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType));
            }
        }

        [RelayCommand]
        public async Task GoToLogDetail()
        {
            try
            {
                await Shell.Current.GoToAsync($"LogDetailPage", true);
            }
            catch (Exception ex)
            {
                await Utility.HandleException(ex, new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType));
            }
        }
    }
}
