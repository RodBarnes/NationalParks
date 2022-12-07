# National Parks
Copied from MauiMVVM to build a National Parks app to list the parks, activities, etc.

### NOTE on the API Key
The app relies upon data from the National Park Service (NPS) via its API and use of the API requires obtaining an API Key from https://www.nps.gov/subjects/developer/get-started.htm.

**For security purposes, an API Key is not included in this repository.  You must obtain your own if forking this code.**

After forking the code, a ```Config.cs``` class needs to be created with the following structure and containing the API Key.  (This file is set to be isgnored in the .gitignore and will not be pushed to the repository.)  The data service expects to find this static class exposing a static member of ApiKey.  

```
namespace NationalParks
{
    public static class Config
    {
        public static string ApiKey { get => "YOUR_API_KEY"; }
    }
}
```
