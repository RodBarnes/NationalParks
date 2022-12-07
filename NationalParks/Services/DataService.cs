using System.Net.Http.Json;

namespace NationalParks.Services;

public class DataService
{
    HttpClient httpClient;

    public DataService()
    {
        this.httpClient = new HttpClient();
    }

    List<Park> parks;

    public async Task<List<Park>> GetData()
    {
        if (parks?.Count > 0)
            return parks;

        Result result = null;

        /*      // Read data from test file
                var basepath = AppDomain.CurrentDomain.BaseDirectory;
                var filepath = basepath.Split("\\bin")[0];
                var filename = @$"{filepath}\Raw\NPS_response.json";
                var jsonstr = File.ReadAllText(filename);
                var result = JsonSerializer.Deserialize<Result>(jsonstr, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        */

        // Get list of parks
        var response = await httpClient.GetAsync($"https://developer.nps.gov/api/v1/parks?api_key={Config.ApiKey}");
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<Result>();
        }

        if (result != null)
        {
            parks = result.Data;
        }

        return parks;
    }
}
