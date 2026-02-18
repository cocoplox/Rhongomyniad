namespace Rhongomyniad.Application.DTOs;

public sealed record RestoreRequestDto
{
    public Guid BackupId { get; init; }
    public bool CreateSafetyBackup { get; init; } = true;
}
