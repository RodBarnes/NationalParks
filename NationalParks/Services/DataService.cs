using Microsoft.VisualBasic;
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
        string url = BuildUrlWithFilter(term, start, limit, states, topics, activities, query);

        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            // This will return true even if it receives the "over rate limit" error.
            // Although, a typical user will never encounter that, right?
            result = await response.Content.ReadFromJsonAsync<T>();
            if (result is null)
            {
                // Try to get the error that may've been sent
                string msg = $"Result was 'null' for type '{typeof(T)}'";
                ResultError errresult = await response.Content.ReadFromJsonAsync<ResultError>();
                if (errresult != null)
                {
                    msg += $"\n[{errresult.Error.Code}--{errresult.Error.Message}";
                }
                throw new Exception(msg);
            }
        }

        return result;
    }

    private static string BuildUrlWithFilter(string term, int start, int limit, string states, string topics, string activities, string query)
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

    public static async Task<T> GetItemsForParkCodeAsync<T>(string term, string parkCode, int start = 0, int limit = 20)
    {
        T result = default;
        string url = $"{DomainUrl}{term}?api_key={Config.NpsApiKey}&start={start}&limit={limit}&parkCode={parkCode}";

        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<T>();
        }

        return result;
    }

    public static async Task<T> GetItemsForIdsAsync<T>(string term, string idList)
    {
        T result = default;

        var url = $"{DomainUrl}{term}/parks?api_key={Config.NpsApiKey}&id={idList}";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<T>();
        }

        return result;
    }
}
