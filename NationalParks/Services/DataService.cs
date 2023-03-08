using System.Net.Http.Json;

namespace NationalParks.Services;

public class DataService
{
    private static HttpClient httpClient;
    private const string DomainUrl = "https://developer.nps.gov/api/v1/";

    public DataService()
    {
        httpClient = new HttpClient();
    }

    public static async Task<T> GetItemsAsync<T>(string term, int start = 0, int limit = 10, string states = "", string topics = "", string activities = "", string query = "")
    {
        T result = default;
        string url = BuildUrl(term, start, limit, states, topics, activities, query);

        // Retrieve data
        if (httpClient is null)
            httpClient = new HttpClient();

        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<T>();
        }

        return result;
    }

    private static string BuildUrl(string term, int start, int limit, string states, string topics, string activities, string query)
    {
        // Build URL
        string paramList = $"&start={start}&limit={limit}";
        if (!String.IsNullOrEmpty(states))
        {
            paramList += $"&stateCode={states}";
        }
        if (!String.IsNullOrEmpty(topics))
        {
            paramList += $"&parkCode={topics}";
        }
        if (!String.IsNullOrEmpty(activities))
        {
            if (!String.IsNullOrEmpty(topics))
            {
                paramList += $",{activities}";
            }
            else
            {
                paramList += $"&parkCode={activities}";
            }
        }
        if (!String.IsNullOrEmpty(query))
        {
            paramList += $"&q={query}";
        }

        return $"{DomainUrl}{term}?api_key={Config.NpsApiKey}{paramList}";
    }

    public static async Task<Result> GetPropertiesForParkCodeAsync(string term, string parkCode, int start = 0, int limit = 20)
    {
        Result result = new();

        var url = $"{DomainUrl}{term}?api_key={Config.NpsApiKey}&start={start}&limit={limit}&parkCode={parkCode}";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            switch(term)
            {
                case ResultAlerts.Term:
                    ResultAlerts resultAlerts = await response.Content.ReadFromJsonAsync<ResultAlerts>();
                    result = resultAlerts;
                    break;
                case ResultParkingLots.Term:
                    ResultParkingLots resultLots = await response.Content.ReadFromJsonAsync<ResultParkingLots>();
                    result = resultLots;
                    break;
                default:
                    throw new Exception($"DataService.GetPropertiesForParkCodeAsync -- No idea what that means: {term}");
            }
        }

        return result;
    }

    public static async Task<ResultParks> GetParkForParkCodeAsync(string parkCode)
    {
        ResultParks result = new();

        var url = $"{DomainUrl}parks?api_key={Config.NpsApiKey}&parkCode={parkCode}";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultParks>();
        }

        return result;
    }

    public static async Task<ResultTopics> GetTopicsForIds(string idList)
    {
        ResultTopics result = new();

        var url = $"{DomainUrl}topics/parks?api_key={Config.NpsApiKey}&id={idList}";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultTopics>();
        }

        return result;
    }

    public static async Task<ResultActivities> GetActivitiesForIds(string idList)
    {
        ResultActivities result = new();

        var url = $"{DomainUrl}activities/parks?api_key={Config.NpsApiKey}&id={idList}";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultActivities>();
        }

        return result;
    }
}
