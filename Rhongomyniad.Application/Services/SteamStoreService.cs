using System.Text.Json;
using Microsoft.Extensions.Logging;
using Rhongomyniad.Domain.Interfaces;
using Rhongomyniad.Domain.Responses;

namespace Rhongomyniad.Application.Services;

public class SteamStoreService : ISteamStoreService
{
    private readonly ILogger _logger;
    private readonly string AppDetailsRoute = "appdetails";
    
    private readonly HttpClient _httpClient;

    public SteamStoreService(HttpClient httpClient, ILogger<SteamStoreService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
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
                _logger.LogInformation($"Getting appdetails for {id}");
                var resp = await _httpClient.GetAsync($"{AppDetailsRoute}/?appids={id}");
                //realmente da un poco igual, la api de steam funciona raro, te devuelve un 200, y dentro de la respuesta, el esatado, succes, failed, etc
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
                _logger.LogError(e, $"Getting appdetails failed for id: {id}");
                //Petó
                return default;
            }
        });
        
        var results = await Task.WhenAll(tasks);
        return results.Where(e => e.Key != 0).ToDictionary(x => x.Key, x => x.Value);
    }
}