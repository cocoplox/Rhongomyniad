using Rhongomyniad.Domain;
using Rhongomyniad.Domain.Entities;
using Rhongomyniad.Domain.Enums;
using Rhongomyniad.Domain.Interfaces;

namespace Rhongomyniad.Infrastructure.Scanners;

/// <summary>
/// Esta clase agrupa todos los scaneres
/// </summary>
public sealed class CompositeGameScanner : IGameScanner
{
    private readonly IEnumerable<IGameScanner> _scanners;

    public GameLauncher _launcherType => GameLauncher.Unknown;

    //todo: Esta clase me la voy a ventilar => Cambiar por un orchestrador
    public CompositeGameScanner(IEnumerable<IGameScanner> scanners)
    {
        _scanners = scanners ?? throw new ArgumentNullException(nameof(scanners));
    }

    
    public async Task<IReadOnlyList<LocalGame>> ScanAsync()
    {
        var localGames = new List<LocalGame>();
        foreach (var scanner in _scanners)
        {
            localGames.AddRange(await scanner.ScanAsync());
        }
        return localGames;
    }

    public bool IsLauncherInstalledAsync()
    {
        return true;
    }
}
