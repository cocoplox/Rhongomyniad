using Microsoft.Win32;
using Rhongomyniad.Application.Mapping;
using Rhongomyniad.Domain;
using Rhongomyniad.Domain.Entities;
using Rhongomyniad.Domain.Enums;
using Rhongomyniad.Domain.Interfaces;
using Rhongomyniad.Domain.ValueObjects;

namespace Rhongomyniad.Infrastructure.Scanners;

public sealed class SteamGameScanner : IGameScanner
{
    private readonly ISteamLibraryPathProvider _pathProvider;
    private readonly ISteamFilesParser _steamFilesParser;

    public GameLauncher _launcherType => GameLauncher.Steam;
    public bool _isLauncherInstalled { get; private set; }

    public SteamGameScanner(ISteamLibraryPathProvider pathProvider, ISteamFilesParser steamFilesParser)
    {
        _pathProvider = pathProvider;
        _steamFilesParser = steamFilesParser;
        _isLauncherInstalled = IsLauncherInstalledAsync();
    }

    public async Task<IReadOnlyList<LocalGame>> ScanAsync()
    {
        var librariesPaths = GetLibraries();
        var manifests = await GetManifests(librariesPaths);
        
        if (!manifests.Any())
            return Array.Empty<LocalGame>();
        var games = new List<LocalGame>();
        foreach (var manifest in manifests)
        {
            games.Add(_steamFilesParser.GetLocalGame(manifest));
        }
        return games;
    }

    private List<string> GetLibraries()
        =>  _pathProvider.GetLibraryPathsAsync();
    private async Task<List<string>> GetManifests(IEnumerable<string> libraries)
        => libraries.Select(_pathProvider.GetAppManifestPaths).SelectMany(x => x).ToList();

    public bool IsLauncherInstalledAsync()
    {
        //Winodws
        if (OperatingSystem.IsWindows())
        {
            var windowsSteamPath =
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Valve\Steam")?.GetValue("InstallPath")?.ToString() ??
                string.Empty;
            return File.Exists(Path.Combine(windowsSteamPath, "steam.exe"));
        }

        if (OperatingSystem.IsLinux())
        {
            //todo: implemetación de linux
        }

        return false;
    }
}