using System.Net.Http.Json;

namespace NationalParks.Services;

public class DataService
{
    private static HttpClient httpClient;

    public DataService()
    {
        httpClient = new HttpClient();
    }

    private static string ConstructUrl(string item, string paramList)
    {
        var url = $"https://developer.nps.gov/api/v1/{item}?api_key={Config.ApiKey}";
        if (paramList != null)
        {
            url += "&" + paramList ;
        }

        return url;
    }

    public static async Task<ResultParks> GetParksAsync(int start = 0, int limit = 20, string topics = "", string activities = "", string states="")
    {
        ResultParks result = new();

        var url = ConstructUrl("parks", $"start={start}&limit={limit}");
        if (!String.IsNullOrEmpty(topics))
        {
            url += $"&topic%3D{topics}";
        }
        if (!String.IsNullOrEmpty(activities))
        {
            url += $"&activities%3D{activities}";
        }
        if (!String.IsNullOrEmpty(states))
        {
            url += $"&stateCode={states}";
        }

        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultParks>();
        }

        return result;
    }

    public static async Task<ResultAlerts> GetAlertsForParkCodeAsync(string parkCode, int start = 0, int limit = 20)
    {
        ResultAlerts result = new();

        var url = ConstructUrl("alerts", $"start={start}&limit={limit}&parkCode={parkCode}");
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultAlerts>();
        }

        return result;
    }

    public static async Task<ResultParks> GetParkForParkCodeAsync(string parkCode)
    {
        ResultParks result = new();

        var url = ConstructUrl("parks", $"parkCode={parkCode}");
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultParks>();
        }

        return result;
    }

    public static async Task<ResultTopics> GetTopicsAsync(int start = 0, int limit = 20)
    {
        ResultTopics result = new();

        var url = ConstructUrl("topics", $"start={start}&limit={limit}");
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultTopics>();
        }

        return result;
    }

    public static async Task<ResultActivities> GetActivitiesAsync(int start = 0, int limit = 20)
    {
        ResultActivities result = new();

        var url = ConstructUrl("activities", $"start={start}&limit={limit}");
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultActivities>();
        }

        return result;
    }

    public static async Task<ResultCampgrounds> GetCampgroundsAsync(int start = 0, int limit = 20, string states = "")
    {
        ResultCampgrounds result = new();

        var url = ConstructUrl("campgrounds", $"start={start}&limit={limit}");
        if (!String.IsNullOrEmpty(states))
        {
            url += $"&stateCode={states}";
        }

        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultCampgrounds>();
        }

        return result;  
    }

    public static async Task<ResultPlaces> GetPlacesAsync(int start = 0, int limit = 20, string states = "")
    {
        ResultPlaces result = new();

        var url = ConstructUrl("places", $"start={start}&limit={limit}");
        if (!String.IsNullOrEmpty(states))
        {
            url += $"&stateCode={states}";
        }

        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultPlaces>();
        }

        return result;
    }

    public static async Task<ResultTours> GetToursAsync(int start = 0, int limit = 20, string states = "")
    {
        ResultTours result = new();

        var url = ConstructUrl("tours", $"start={start}&limit={limit}");
        if (!String.IsNullOrEmpty(states))
        {
            url += $"&stateCode={states}";
        }

        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultTours>();
        }

        return result;
    }

    public static async Task<ResultWebcams> GetWebcamsAsync(int start = 0, int limit = 20)
    {
        ResultWebcams result = new();

        var url = ConstructUrl("webcams", $"start={start}&limit={limit}");
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultWebcams>();
        }

        return result;
    }

    public static async Task<ResultEvents> GetEventsAsync(int start = 0, int limit = 20, string states = "")
    {
        ResultEvents result = new();

        var url = ConstructUrl("events", $"start={start}&limit={limit}");
        if (!String.IsNullOrEmpty(states))
        {
            url += $"&stateCode={states}";
        }

        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultEvents>();
        }

        return result;
    }

    public async Task<ResultThingsToDo> GetThingsToDoAsync(int start = 0, int limit = 20, string states = "")
    {
        ResultThingsToDo result = new();

        var url = ConstructUrl("thingstodo", $"start={start}&limit={limit}");
        if (!String.IsNullOrEmpty(states))
        {
            url += $"&stateCode={states}";
        }

        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultThingsToDo>();
        }

        return result;
    }
}
