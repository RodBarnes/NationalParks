using System.Net.Http.Json;
using System.Text.Json;

namespace NationalParks.Services;

public class DataService
{
    private static HttpClient httpClient;

    public DataService()
    {
        httpClient = new HttpClient();
    }

    public async Task<ResultParks> GetParksAsync(int start = 0, int limit = 20, string topics = "", string activities = "", string states="")
    {
        var result = new ResultParks();

        // States: Comma-delimited list -- stateCode=OR%2CWA

        var url = $"https://developer.nps.gov/api/v1/parks?api_key={Config.ApiKey}&start={start}&limit={limit}";
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

    public async Task<ResultTopics> GetTopicsAsync(int start = 0, int limit = 20)
    {
        var result = new ResultTopics();

        var url = $"https://developer.nps.gov/api/v1/topics?api_key={Config.ApiKey}&start={start}&limit={limit}";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultTopics>();
        }

        return result;
    }

    public async Task<ResultActivities> GetActivitiesAsync(int start = 0, int limit = 20)
    {
        var result = new ResultActivities();

        var url = $"https://developer.nps.gov/api/v1/activities?api_key={Config.ApiKey}&start={start}&limit={limit}";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultActivities>();
        }

        return result;
    }

    public async Task<ResultWebcams> GetWebcamsAsync(int start = 0, int limit = 20)
    {
        var result = new ResultWebcams();

        var url = $"https://developer.nps.gov/api/v1/webcams?api_key={Config.ApiKey}&start={start}&limit={limit}";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultWebcams>();
        }

        return result;
    }

    public async Task<ResultCampgrounds> GetCampgroundsAsync(int start = 0, int limit = 20)
    {
        var result = new ResultCampgrounds();

        var url = $"https://developer.nps.gov/api/v1/campgrounds?api_key={Config.ApiKey}&start={start}&limit={limit}";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultCampgrounds>();
        }

        return result;
    }
}
