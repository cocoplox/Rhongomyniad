using Rhongomyniad.Domain.Enums;

namespace Rhongomyniad.Application.DTOs;

public sealed record GameSummaryDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? Publisher { get; init; }
    public string? Developer { get; init; }
    public GameLauncher Launcher { get; init; }
    public string InstallPath { get; init; } = string.Empty;
    public string? LauncherGameId { get; init; }
    public bool HasSaveProfile { get; init; }
    public bool HasConfigProfile { get; init; }
    public DateTime DetectedAt { get; init; }
    public DateTime? LastPlayedAt { get; init; }
}
