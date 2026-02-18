using System.Text.Json;
using Rhongomyniad.Domain.Interfaces;
using Rhongomyniad.Domain.Responses;

namespace Rhongomyniad.Application.Services;

public class SteamStoreService : ISteamStoreService
{
    private readonly string AppDetailsRoute = "appdetails";
    
    private readonly HttpClient _httpClient;

    public SteamStoreService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<Dictionary<int, SteamAppDetails>> GetAppDetails(IEnumerable<int> appIds)
    {
        if (!appIds.Any())
            return new();
        var successfulResponses = new  Dictionary<string, SteamAppDetails>();

        var tasks = appIds.Select(async id =>
        {
            try
            {
                var resp = await _httpClient.GetAsync($"{_httpClient.BaseAddress}{AppDetailsRoute}/?appids={id}");
                resp.EnsureSuccessStatusCode();

                var json = await resp.Content.ReadAsStringAsync();

                var dict = JsonSerializer.Deserialize<Dictionary<string, SteamAppDetails>>(json);

                if (dict == null)
                    return default;

                foreach (var kvp in dict)
                {
                    if (kvp.Value.Success && int.TryParse(kvp.Key, out var appId))
                        return new KeyValuePair<int, SteamAppDetails>(appId, kvp.Value);
                }
                return default;
            }
            catch (Exception e)
            {
                //Petó
                return default;
            }
        });
        
        var results = await Task.WhenAll(tasks);
        return results.Where(e => e.Key != 0).ToDictionary(x => x.Key, x => x.Value);
    }
}