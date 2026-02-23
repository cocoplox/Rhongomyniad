using Rhongomyniad.Domain.Entities;
using Rhongomyniad.Domain.Enums;

namespace Rhongomyniad.Domain.Interfaces;

public interface IGameScanner
{
    GameLauncher _launcherType { get; }
    Task<IReadOnlyList<LocalGame>> ScanAsync();
    bool IsLauncherInstalledAsync();
}
