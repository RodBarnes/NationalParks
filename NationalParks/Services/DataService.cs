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

    public async Task<ResultParks> GetParksAsync(int start = 0, int limit = 20)
    {
        ResultParks result = new ResultParks();

        // Read data from test file
        //var json = ReadJsonFile("Parks.json");
        //result = JsonSerializer.Deserialize<ResultParks>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });


        var url = $"https://developer.nps.gov/api/v1/parks?api_key={Config.ApiKey}&start={start}&limit={limit}";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultParks>();
        }

        return result;
    }

    public async Task<ResultTopics> GetTopicsAsync(int start = 0, int limit = 20)
    {
        ResultTopics result = new ResultTopics();

        // Read data from test file
        //var json = ReadJsonFile("Topics.json");
        //result = JsonSerializer.Deserialize<ResultTopics>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

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
        ResultActivities result = new ResultActivities();

        // Read data from test file
        //var json = ReadJsonFile("Topics.json");
        //result = JsonSerializer.Deserialize<ResultTopics>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var url = $"https://developer.nps.gov/api/v1/activities?api_key={Config.ApiKey}&start={start}&limit={limit}";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ResultActivities>();
        }

        return result;
    }

    private string ReadJsonFile(string filename)
    {
        // Read data from test file
        var basepath = AppDomain.CurrentDomain.BaseDirectory;
        var filepath = basepath.Split("\\bin")[0];
        var pathname = @$"{filepath}\Raw\{filename}";
        var jsonstr = File.ReadAllText(pathname);

        return jsonstr;
    }
}
