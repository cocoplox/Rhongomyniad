using Rhongomyniad.Domain.Entities;
using Rhongomyniad.Domain.Enums;

namespace Rhongomyniad.Domain.Interfaces;

public interface IGameScanner
{
    GameLauncher LauncherType { get; }
    Task<IReadOnlyList<Game>> ScanAsync();
    Task<bool> IsLauncherInstalledAsync();
}
