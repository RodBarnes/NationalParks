using static Android.Provider.Settings;

namespace NationalParks.Platforms.Android;

internal class GetDeviceInfo : IGetDeviceInfo
{
    public string GetDeviceID()
    {
        var context = Android.App.Application.Context;

        string id = Android.Provider.Settings.Secure.GetString(context.ContentResolver, Secure.AndroidId);

        return id;
    }
}