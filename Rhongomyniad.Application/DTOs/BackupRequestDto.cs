using Rhongomyniad.Domain.Enums;

namespace Rhongomyniad.Application.DTOs;

public sealed record BackupRequestDto
{
    public Guid GameId { get; init; }
    public BackupType Type { get; init; }
    public string? Notes { get; init; }
}
