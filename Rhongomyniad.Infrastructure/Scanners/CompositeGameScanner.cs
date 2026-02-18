using Rhongomyniad.Domain;
using Rhongomyniad.Domain.Entities;
using Rhongomyniad.Domain.Enums;
using Rhongomyniad.Domain.Interfaces;

namespace Rhongomyniad.Infrastructure.Scanners;

public sealed class CompositeGameScanner : IGameScanner
{
    private readonly IEnumerable<IGameScanner> _scanners;

    public GameLauncher LauncherType => GameLauncher.Unknown;

    public CompositeGameScanner(IEnumerable<IGameScanner> scanners)
    {
        _scanners = scanners ?? throw new ArgumentNullException(nameof(scanners));
    }

    public async Task<IReadOnlyList<Game>> ScanAsync()
    {
        var allGames = new List<Game>();

        foreach (var scanner in _scanners)
        {
            try
            {
                var games = await scanner.ScanAsync();
                allGames.AddRange(games);
            }
            catch (Exception)
            {
                // Log error but continue with other scanners
            }
        }

        return allGames.AsReadOnly();
    }

    public Task<bool> IsLauncherInstalledAsync()
    {
        return Task.FromResult(true);
    }
}
