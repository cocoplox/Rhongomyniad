using Rhongomyniad.Domain;
using Rhongomyniad.Domain.Entities;
using Rhongomyniad.Domain.Enums;
using Rhongomyniad.Domain.Interfaces;
using Rhongomyniad.Domain.ValueObjects;

namespace Rhongomyniad.Infrastructure.Scanners;

public sealed class EpicGameScanner : IGameScanner
{
    public GameLauncher LauncherType => GameLauncher.EpicGames;

    public Task<IReadOnlyList<Game>> ScanAsync()
    {
        // TODO: Implement Epic Games scanning
        var games = new List<Game>();
        return Task.FromResult<IReadOnlyList<Game>>(games.AsReadOnly());
    }

    public Task<bool> IsLauncherInstalledAsync()
    {
        var epicPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            "Epic Games", "Launcher", "Engine", "Binaries", "Win64", "EpicGamesLauncher.exe");
        return Task.FromResult(File.Exists(epicPath));
    }
}
