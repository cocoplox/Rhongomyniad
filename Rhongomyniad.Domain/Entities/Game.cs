using Rhongomyniad.Domain.Enums;
using Rhongomyniad.Domain.ValueObjects;

namespace Rhongomyniad.Domain.Entities;

public sealed record Game
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public string? Publisher { get; init; }
    public string? Developer { get; init; }
    public GameLauncher Launcher { get; init; }
    public GamePath InstallPath { get; init; }
    public string? LauncherGameId { get; init; }
    public SaveProfile? SaveProfile { get; init; }
    public ConfigProfile? ConfigProfile { get; init; }
    public DateTime DetectedAt { get; init; }
    public DateTime? LastPlayedAt { get; init; }

    public Game(
        Guid id,
        string name,
        GameLauncher launcher,
        GamePath installPath)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(installPath);

        Id = id;
        Name = name;
        Launcher = launcher;
        InstallPath = installPath;
        DetectedAt = DateTime.UtcNow;
    }
}
