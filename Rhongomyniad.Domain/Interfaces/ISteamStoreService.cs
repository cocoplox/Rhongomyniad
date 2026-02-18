using Rhongomyniad.Domain.Responses;

namespace Rhongomyniad.Domain.Interfaces;

public interface ISteamStoreService
{
    Task<Dictionary<int,SteamAppDetails>> GetAppDetails(IEnumerable<int> appIds);
}