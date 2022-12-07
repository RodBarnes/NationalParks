using System.Net.Http.Json;

namespace NationalParks.Services;

public class DataService
{
    private static HttpClient httpClient;

    public DataService()
    {
        httpClient = new HttpClient();
    }

    Result result;

    public async Task<Result> GetData(int start = 0)
    {
        /*      // Read data from test file
                var basepath = AppDomain.CurrentDomain.BaseDirectory;
                var filepath = basepath.Split("\\bin")[0];
                var filename = @$"{filepath}\Raw\NPS_response.json";
                var jsonstr = File.ReadAllText(filename);
                var result = JsonSerializer.Deserialize<Result>(jsonstr, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        */

        // Get list of parks from position
        var url = $"https://developer.nps.gov/api/v1/parks?api_key={Config.ApiKey}&start={start}";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<Result>();
        }

        return result;
    }
}
