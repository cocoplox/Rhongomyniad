using Rhongomyniad.Domain.Entities;

namespace Rhongomyniad.Domain.Interfaces;

public interface ISteamFilesParser
{
    /// <summary>
    /// Converts game's manifests into DTOs
    /// </summary>
    /// <param name="manifestPath"></param>
    /// <returns>Every appManifest info into dto LocalGame</returns>
    LocalGame GetLocalGame(string manifestPath);

    List<string> GetSteamLibrariesFromVdf(string vdfPath);
}