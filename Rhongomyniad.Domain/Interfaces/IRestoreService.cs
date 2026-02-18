using Rhongomyniad.Domain.Entities;

namespace Rhongomyniad.Domain.Interfaces;

public interface IRestoreService
{
    Task<Result> RestoreAsync(BackupSnapshot snapshot);
    Task<Result<BackupSnapshot>> CreateSafetyBackupAsync(Game game, string destinationPath);
}
