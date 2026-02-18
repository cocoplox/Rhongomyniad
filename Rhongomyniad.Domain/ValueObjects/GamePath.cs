namespace Rhongomyniad.Domain.ValueObjects;

public sealed class GamePath
{
    public string Value { get; }
    public bool Exists => Directory.Exists(Value);

    public GamePath(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        
        Value = Path.GetFullPath(path);
    }

    public override bool Equals(object? obj)
    {
        if (obj is GamePath other)
        {
            return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(GamePath path) => path.Value;
    public static explicit operator GamePath(string path) => new(path);
}
