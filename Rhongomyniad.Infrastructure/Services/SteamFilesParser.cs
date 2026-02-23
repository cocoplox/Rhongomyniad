using Rhongomyniad.Domain.Entities;
using Rhongomyniad.Domain.Enums;
using Rhongomyniad.Domain.Interfaces;

namespace Rhongomyniad.Infrastructure.Services;

public class SteamFilesParser : ISteamFilesParser
{
    /// <inheritdoc/>
    public LocalGame GetLocalGame(string manifestPath)
    {
        List<LocalGame> localGames = new List<LocalGame>();
        if (!File.Exists(manifestPath))
            throw new FileNotFoundException("The specified manifest directory was not found.");

        string[] fileContent = File.ReadAllLines(manifestPath);
        var fileInfo = GetFileInfo(fileContent);
        return new()
        {
            AppId = long.Parse(fileInfo["appid"]),
            GameLauncher = GameLauncher.Steam.ToString(),
            Name = fileInfo["name"],
            InstallDir = Path.Combine(Path.GetDirectoryName(manifestPath)!, fileInfo["installdir"]),
            SaveFilesDir = string.Empty,
            ConfigFilesDir = string.Empty,
        };
    }

    /// <inheritdoc/>>
    public List<string> GetSteamLibrariesFromVdf(string vdfPath)
    {
        var fileContent = File.ReadAllLines(vdfPath);
        var rawPaths = fileContent
            .Where(line => line.Contains("path"))
            .Select(line => line.Split('"', StringSplitOptions.RemoveEmptyEntries))
            .SelectMany(e => e)
            .Where(e => !string.IsNullOrWhiteSpace(e))
            .Where(e => !e.Contains("path"))
            .Select(e => e.Replace("\\\\","\\"));
        
        return rawPaths.Select(e => Path.Combine($"{e}\\","steamapps")).ToList();
    }

    /// <summary>
    /// Gets only the Key-Value lines on a vdf file, it doesnt nest childs as it not usually necessary
    /// </summary>
    /// <param name="fileContent"></param>
    /// <param name="keys"></param>
    /// <returns></returns>
    private Dictionary<string, string> GetFileInfo(string[] fileContent)
    {
        var kvp = new List<KeyValuePair<string, string>>();
        foreach (string line in fileContent)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
            //Linea tipica
            var parts = line.Split('"', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 2)
            {
                var usefullParts = parts.Where(e => !e.Contains("\t")).ToList();
                kvp.Add(new(usefullParts[0], usefullParts[1]));
            }
        }
        return kvp
            .GroupBy(kvp => kvp.Key)
            .ToDictionary(g => g.Key, g => g.First().Value);
    }
}