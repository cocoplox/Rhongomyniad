namespace Rhongomyniad.Domain.Entities;

public sealed record SaveProfile
{
    public Guid Id { get; init; }
    public Guid GameId { get; init; }
    public IReadOnlyList<string> SaveFilePaths { get; init; }
    public string? PrimarySaveDirectory { get; init; }
    public DateTime? LastModifiedAt { get; init; }
    public long TotalSizeBytes { get; init; }
    public string? SaveFormat { get; init; }

    public SaveProfile(
        Guid id,
        Guid gameId,
        IEnumerable<string> saveFilePaths)
    {
        ArgumentNullException.ThrowIfNull(saveFilePaths);

        Id = id;
        GameId = gameId;
        SaveFilePaths = saveFilePaths.ToList().AsReadOnly();
        TotalSizeBytes = CalculateTotalSize();
    }

    private long CalculateTotalSize()
    {
        long totalSize = 0;
        foreach (var path in SaveFilePaths)
        {
            try
            {
                var fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    totalSize += fileInfo.Length;
                }
            }
            catch
            {
                // Ignore files that can't be accessed
            }
        }
        return totalSize;
    }
}
