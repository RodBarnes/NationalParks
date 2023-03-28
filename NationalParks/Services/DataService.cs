using System.Net.Http.Json;

namespace NationalParks.Services;

public class DataService
{
    private static HttpClient httpClient;
    private const string DomainUrl = "https://developer.nps.gov/api/v1/";

    static DataService()
    {
        httpClient = new HttpClient();
    }

    public static async Task<T> GetItemsAsync<T>(Terms term, int start = 0, int limit = 10, string states = "", string topics = "", string activities = "", string query = "")
    {
        T result = default;
        string url = BuildUrlWithFilter(term, start, limit, states, topics, activities, query);

        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            // This will return true even if it receives the "over rate limit" error.
            // Although, a typical user will never encounter that, right?
            //    {
            //        "error": {
            //            "code": "OVER_RATE_LIMIT",
            //            "message": "You have exceeded your rate limit. Try again later or contact us at https://developer.nps.gov:443/contact/ for assistance"
            //        }
            //    }

            try
            {
                result = await response.Content.ReadFromJsonAsync<T>();
            }
            catch (InvalidCastException)
            {
                // Try to get the error that may've been sent
                string msg = $"Result was unexpected for type '{typeof(T)}'";
                ResultError errresult = await response.Content.ReadFromJsonAsync<ResultError>();
                if (errresult != null)
                {
                    msg += $"\nServer responded: {errresult.Error.Code}--{errresult.Error.Message}";
                }

                await Logger.WriteLogEntry(msg);

                switch (errresult.Error.Code)
                {
                    case "OVER_RATE_LIMIT":
                        msg = "that you are trying to do too much, too quickly.  Wait a while for it to recover from your repeated access.";
                        break;
                    default:
                        msg = $"with this: {errresult.Error.Code}--{errresult.Error.Message}";
                        break;
                }
                await Shell.Current.DisplayAlert("Server Error", $"The server replied {msg}", "OK");
            }
        }

        return result;
    }

    private static string BuildUrlWithFilter(Terms term, int start, int limit, string states, string topics, string activities, string query)
    {
        string strTerm = "";

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

        switch (term)
        {
            case Terms.videos:
                strTerm = $"multimedia/{term}";
                break;
            case Terms.audio:
                strTerm = $"multimedia/{term}";
                break;
            default:
                strTerm = term.ToString();
                break;
        }

        return $"{DomainUrl}{strTerm}?api_key={Config.NpsApiKey}{paramList}";
    }

    public static async Task<T> GetItemsForParkCodeAsync<T>(Terms term, string parkCode, int start = 0, int limit = 20)
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

    public static async Task<T> GetItemsForIdsAsync<T>(Terms term, string idList)
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
