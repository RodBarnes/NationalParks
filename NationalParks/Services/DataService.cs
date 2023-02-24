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

    public static async Task<Result> GetItemsAsync(string term, int start = 0, int limit = 20, string states = "", string topics = "", string activities = "")
    {
        Result result = new();

        // Build URL
        string paramList = $"&start={start}&limit={limit}";
        if (!String.IsNullOrEmpty(states))
        {
            paramList += $"&stateCode={states}";
        }
        if (!String.IsNullOrEmpty(topics))
        {
            paramList += $"&topic%3D{topics}";
        }
        if (!String.IsNullOrEmpty(activities))
        {
            paramList += $"&activities%3D{activities}";
        }
        var url = $"{DomainUrl}{term}?api_key={Config.NpsApiKey}{paramList}";

        // Retrieve data
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            switch(term)
            {
                case ResultParks.Term:
                    ResultParks resultParks = await response.Content.ReadFromJsonAsync<ResultParks>();
                    result = resultParks;
                    break;
                case ResultCampgrounds.Term:
                    ResultCampgrounds resultCampgrounds = await response.Content.ReadFromJsonAsync<ResultCampgrounds>();
                    result = resultCampgrounds;
                    break;
                case ResultPlaces.Term:
                    ResultPlaces resultPlaces = await response.Content.ReadFromJsonAsync<ResultPlaces>();
                    result = resultPlaces;
                    break;
                case ResultTours.Term:
                    ResultTours resultTours = await response.Content.ReadFromJsonAsync<ResultTours>();
                    result = resultTours;
                    break;
                case ResultThingsToDo.Term:
                    ResultThingsToDo resultThingsToDo = await response.Content.ReadFromJsonAsync<ResultThingsToDo>();
                    result = resultThingsToDo;
                    break;
                case ResultWebcams.Term:
                    ResultWebcams resultWebcams = await response.Content.ReadFromJsonAsync<ResultWebcams>();
                    result = resultWebcams;
                    break;
                case ResultEvents.Term:
                    ResultEvents resultEvents = await response.Content.ReadFromJsonAsync<ResultEvents>();
                    result = resultEvents;
                    break;
                case ResultAlerts.Term:
                    ResultAlerts resultAlerts = await response.Content.ReadFromJsonAsync<ResultAlerts>();
                    result = resultAlerts;
                    break;
                case ResultActivities.Term:
                    ResultActivities resultActivities = await response.Content.ReadFromJsonAsync<ResultActivities>();
                    result = resultActivities;
                    break;
                case ResultTopics.Term:
                    ResultTopics resultTopics = await response.Content.ReadFromJsonAsync<ResultTopics>();
                    result = resultTopics;
                    break;
                case ResultPeople.Term:
                    ResultPeople resultPeople = await response.Content.ReadFromJsonAsync<ResultPeople>();
                    result = resultPeople;
                    break;
                case ResultNewsReleases.Term:
                    ResultNewsReleases resultNewsReleases = await response.Content.ReadFromJsonAsync<ResultNewsReleases>();
                    result = resultNewsReleases;
                    break;
                case ResultArticles.Term:
                    ResultArticles resultArticles = await response.Content.ReadFromJsonAsync<ResultArticles>();
                    result = resultArticles;
                    break;
                default:
                    throw new Exception($"DataService.GetItemsAsync -- No idea what that means: {term}");
            }
        }

        return result;
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
}
