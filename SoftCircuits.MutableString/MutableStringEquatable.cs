/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString;

public sealed partial class MutableString : IEquatable<MutableString?>, IEquatable<string?>
{
    /// <summary>
    /// Determines whether this instance is equal to the specified <see cref="MutableString"/>
    /// </summary>
    /// <param name="other">The string to compare to.</param>
    /// <returns><see langword="true" /> if equal, <see langword="false" /> otherwise.</returns>
    public bool Equals(MutableString? other)
    {
        if (other == null)
            return false;
        if (other.Length != InternalLength)
            return false;
        return other.AsSpan().Equals(AsSpan(), StringComparison.Ordinal);
    }

    /// <summary>
    /// Determines whether this instance is equal to the specified <see cref="string"/>
    /// </summary>
    /// <param name="other">The string to compare to.</param>
    /// <returns><see langword="true" /> if equal, <see langword="false" /> otherwise.</returns>
    public bool Equals(string? other)
    {
        if (other == null)
            return false;
        if (other.Length != InternalLength)
            return false;
        return other.AsSpan().Equals(AsSpan(), StringComparison.Ordinal);
    }

    /// <summary>
    /// Determines whether this instance is equal to the specified <see cref="object"/>
    /// </summary>
    /// <param name="obj">The object to compare to.</param>
    /// <returns><see langword="true" /> if equal, <see langword="false" /> otherwise.</returns>
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            MutableString ms => Equals(ms),
            string s => Equals(s),
            _ => false
        };
    }

    /// <summary>
    /// Returns the hash code for this <see cref="MutableString"/> instance.
    /// </summary>
    public override int GetHashCode() => string.GetHashCode(AsSpan());
}
