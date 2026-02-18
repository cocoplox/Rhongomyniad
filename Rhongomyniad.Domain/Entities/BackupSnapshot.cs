using Rhongomyniad.Domain.Enums;
using Rhongomyniad.Domain.ValueObjects;

namespace Rhongomyniad.Domain.Entities;

public sealed record BackupSnapshot
{
    public Guid Id { get; init; }
    public Guid GameId { get; init; }
    public string GameName { get; init; }
    public BackupType Type { get; init; }
    public BackupStatus Status { get; init; }
    public string BackupFilePath { get; init; }
    public BackupMetadata Metadata { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? Notes { get; init; }

    public BackupSnapshot(
        Guid id,
        Guid gameId,
        string gameName,
        BackupType type,
        string backupFilePath,
        BackupMetadata metadata)
    {
        ArgumentNullException.ThrowIfNull(gameName);
        ArgumentNullException.ThrowIfNull(backupFilePath);
        ArgumentNullException.ThrowIfNull(metadata);

        Id = id;
        GameId = gameId;
        GameName = gameName;
        Type = type;
        BackupFilePath = backupFilePath;
        Metadata = metadata;
        Status = BackupStatus.Completed;
        CreatedAt = DateTime.UtcNow;
    }
}
