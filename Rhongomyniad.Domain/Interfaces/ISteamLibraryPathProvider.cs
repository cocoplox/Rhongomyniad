namespace Rhongomyniad.Domain.Interfaces;

public interface ISteamLibraryPathProvider
{
    /// <summary>
    /// </summary>
    /// <returns>List of all steam libraries absolute paths</returns>
    List<string> GetLibraryPathsAsync();
    /// <summary>
    /// </summary>
    /// <param name="libraryPath"></param>
    /// <returns>The absolute routes of every appmanifest, that contains information about installed games</returns>
     List<string>GetAppManifestPaths(string libraryPath);
    /// <summary>
    /// Changes steam installation path, used when steam.exe is not found on the expected route
    /// </summary>
    /// <param name="steamInstallationPath">New steam installation path</param>
    void ChangeSteamInstallationPath(string steamInstallationPath);
}