# National Parks
Copied from MauiMVVM to build a National Parks app to list the parks, activities, etc.

### NOTE on the API Key
The app relies upon data from the National Park Service (NPS) via its API and use of the API requires obtaining an API Key from https://www.nps.gov/subjects/developer/get-started.htm.

**For security purposes, an API Key is not included in this repository.  You must obtain your own if building or forking this code.**

After cloning the repository, create a ```Config.cs``` file in the local project root folder that contains the code (below) and insert your API Key in place of the token "YOUR_API_KEY".  *This file is set as ignored in the .gitignore and will not be pushed to the repository.*

```
namespace NationalParks
{
    public static class Config
    {
        public static string ApiKey { get => "YOUR_API_KEY"; }
    }
}
```
