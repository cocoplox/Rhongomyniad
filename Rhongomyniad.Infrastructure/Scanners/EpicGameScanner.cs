using Rhongomyniad.Domain;
using Rhongomyniad.Domain.Entities;
using Rhongomyniad.Domain.Enums;
using Rhongomyniad.Domain.Interfaces;
using Rhongomyniad.Domain.ValueObjects;

namespace Rhongomyniad.Infrastructure.Scanners;

public sealed class EpicGameScanner : IGameScanner
{
    public GameLauncher _launcherType => GameLauncher.EpicGames;

    public Task<IReadOnlyList<LocalGame>> ScanAsync()
    {
        // TODO: Implement Epic Games scanning
        var games = new List<Game>();
        return default;
    }

    public bool IsLauncherInstalledAsync()
    {
        var epicPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            "Epic Games", "Launcher", "Engine", "Binaries", "Win64", "EpicGamesLauncher.exe");
        return File.Exists(epicPath);
    }
}
