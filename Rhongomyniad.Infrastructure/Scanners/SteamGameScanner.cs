using Microsoft.Win32;
using Rhongomyniad.Domain;
using Rhongomyniad.Domain.Entities;
using Rhongomyniad.Domain.Enums;
using Rhongomyniad.Domain.Interfaces;
using Rhongomyniad.Domain.ValueObjects;

namespace Rhongomyniad.Infrastructure.Scanners;

public sealed class SteamGameScanner : IGameScanner
{
    private readonly ISteamStoreService _steamStoreService;

    public GameLauncher LauncherType => GameLauncher.Steam;
    public Task<bool> IsLauncherInstalled => IsLauncherInstalledAsync();

    public SteamGameScanner(ISteamStoreService steamStoreService)
    {
        _steamStoreService = steamStoreService;
    }

    public async Task<IReadOnlyList<Game>> ScanAsync()
    {
        // TODO: Implement Steam library scanning
        var appIds = GetLocalAppsIds();
        var appsInfo = await _steamStoreService.GetAppDetails(appIds);
        // Mapear esta wea a algo entendible en la UI, hay que pensarlo
        var games = new List<Game>();
        return default;
    }

    public Task<bool> IsLauncherInstalledAsync()
    {
        //Winodws
        if (OperatingSystem.IsWindows())
        {
            var windowsSteamPath =
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Valve\Steam")?.GetValue("InstallPath")?.ToString() ??
                string.Empty;
            return Task.FromResult(File.Exists(Path.Combine(windowsSteamPath, "steam.exe")));
        }

        if (OperatingSystem.IsLinux())
        {
            //todo: implemetación de linux
        }

        return Task.FromResult(false);
    }

    private IEnumerable<int> GetLocalAppsIds()
    {
        //Tendremos que saber donde estan los juegos instalados =>
        var librariesPaths = GetLibrariesPaths();
        return librariesPaths.SelectMany(GetAppIdsFromLibraryPath).ToList();

        //Ahora tendremos que leer estos archivos para sacar los ids
    }

    //Aqui tenemos traya, como el subnormal tenga mas de 1 libreria, vamos a tener que mapear el vdf, buena suerte
    private IEnumerable<string> GetLibrariesPaths()
    {
        if (OperatingSystem.IsWindows())
        {
            //primero que nada, vamos a ver cuantas librerias tiene
            var windowsSteamPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Valve\Steam")?.GetValue("InstallPath")
                ?.ToString();

            var vdfFilePath = Path.Combine(windowsSteamPath, "steamapps", "libraryfolders.vdf");

            if (!File.Exists(vdfFilePath))
                throw new FileNotFoundException("No existe archivo de biblioteca en Steam");

            //Leemos las lineas de esta shit
            var lines = File.ReadAllLines(vdfFilePath);
            return lines
                .Where(e => e.Contains("path"))
                .Select(CleanVdfPathLine)
                .ToList();
        }

        return default;
    }

    private IEnumerable<int> GetAppIdsFromLibraryPath(string libraryPath)
    {
        //Primero vamos a filtrar este directorio, y quedarnos solo con los manifests => appmanifest_*.acf
        var files = Directory.EnumerateFiles(libraryPath, "appmanifest_*.acf");
        var appIds = new List<int>();

        foreach (var file in files)
        {
            var lines = File.ReadAllLines(file);
            int appId;
            appId = lines
                .Where(l => l.Contains("appid"))
                .Select(l => int.Parse(l.Split("\t\t")[1].Replace("\"","")))
                .FirstOrDefault();
            appIds.Add(appId);
        }
        return appIds;
    }

    private string CleanVdfPathLine(string pathLine)
    {
        var rawLine = pathLine
            .Replace("\"", "")
            .Trim()
            .Split("\t\t")[1]
            .Replace("\\\\","\\");
        return Path.Combine(rawLine, "steamapps");
    }
}
