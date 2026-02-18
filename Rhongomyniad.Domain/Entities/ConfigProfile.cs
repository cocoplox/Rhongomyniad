namespace Rhongomyniad.Domain.Entities;

public sealed record ConfigProfile
{
    public Guid Id { get; init; }
    public Guid GameId { get; init; }
    public IReadOnlyList<string> ConfigFilePaths { get; init; }
    public string? PrimaryConfigDirectory { get; init; }
    public DateTime? LastModifiedAt { get; init; }
    public long TotalSizeBytes { get; init; }

    public ConfigProfile(
        Guid id,
        Guid gameId,
        IEnumerable<string> configFilePaths)
    {
        ArgumentNullException.ThrowIfNull(configFilePaths);

        Id = id;
        GameId = gameId;
        ConfigFilePaths = configFilePaths.ToList().AsReadOnly();
        TotalSizeBytes = CalculateTotalSize();
    }

    private long CalculateTotalSize()
    {
        long totalSize = 0;
        foreach (var path in ConfigFilePaths)
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
