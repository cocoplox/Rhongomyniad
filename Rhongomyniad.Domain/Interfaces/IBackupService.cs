using Rhongomyniad.Domain.Entities;
using Rhongomyniad.Domain.Enums;

namespace Rhongomyniad.Domain.Interfaces;

public interface IBackupService
{
    Task<Result<BackupSnapshot>> CreateBackupAsync(
        Game game,
        BackupType type,
        string destinationPath,
        string? notes = null);

    Task<Result> DeleteBackupAsync(BackupSnapshot snapshot);
    Task<bool> VerifyIntegrityAsync(BackupSnapshot snapshot);
}
