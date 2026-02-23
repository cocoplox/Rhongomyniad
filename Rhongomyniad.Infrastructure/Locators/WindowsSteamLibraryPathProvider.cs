using Microsoft.Win32;
using Rhongomyniad.Domain.Interfaces;

namespace Rhongomyniad.Infrastructure.Locators;

public class WindowsSteamLibraryPathProvider : ISteamLibraryPathProvider
{
    private string _steamInstallationPath { get; set; }
    private readonly ISteamFilesParser _steamFilesParser;

    public WindowsSteamLibraryPathProvider(ISteamFilesParser steamFilesParser)
    {
        _steamFilesParser = steamFilesParser;
        //Annoying!!
#pragma warning disable CA1416
        _steamInstallationPath =
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Valve\Steam").GetValue("InstallPath").ToString()
            ?? throw new FileNotFoundException("steam.exe not found on: " + _steamInstallationPath);
#pragma warning restore CA1416
    }
    /// <inheritdoc/>>
    public List<string> GetLibraryPathsAsync()
    {
        var vdfPath = Path.Combine(_steamInstallationPath, "steamapps", "libraryfolders.vdf");
        return _steamFilesParser.GetSteamLibrariesFromVdf(vdfPath);
    }

    /// <inheritdoc/>>
    public List<string> GetAppManifestPaths(string libraryPath)
         => Directory.EnumerateFiles(libraryPath, "appmanifest_*.acf", SearchOption.TopDirectoryOnly).ToList();

    public void ChangeSteamInstallationPath(string steamInstallationPath)
    {
        if (!File.Exists(steamInstallationPath))
            throw new FileNotFoundException("steam.exe not found on: " + steamInstallationPath);
        _steamInstallationPath = steamInstallationPath;
    }
}