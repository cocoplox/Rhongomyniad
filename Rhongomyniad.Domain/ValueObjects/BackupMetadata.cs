namespace Rhongomyniad.Domain.ValueObjects;

public sealed record BackupMetadata
{
    public DateTime Timestamp { get; init; }
    public long SizeBytes { get; init; }
    public string Sha256Hash { get; init; }
    public int FileCount { get; init; }
    public string? Version { get; init; }

    public BackupMetadata(
        DateTime timestamp,
        long sizeBytes,
        string sha256Hash,
        int fileCount)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sha256Hash);

        Timestamp = timestamp;
        SizeBytes = sizeBytes;
        Sha256Hash = sha256Hash;
        FileCount = fileCount;
    }
}
