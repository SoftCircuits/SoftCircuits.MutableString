namespace SoftCircuits.MutableString;

public sealed partial class MutableString : IEquatable<MutableString?>, IEquatable<string?>
{
    public bool Equals(MutableString? other)
    {
        if (other == null)
            return false;
        if (other.Length != Count)
            return false;
        return other.AsSpan().Equals(AsSpan(), StringComparison.Ordinal);
    }

    public bool Equals(string? other)
    {
        if (other == null)
            return false;
        if (other.Length != Count)
            return false;
        return other.AsSpan().Equals(AsSpan(), StringComparison.Ordinal);
    }

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            MutableString ms => Equals(ms),
            string s => Equals(s),
            _ => false
        };
    }

    public override int GetHashCode() => string.GetHashCode(AsSpan());
}
