/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString;

public sealed partial class MutableString
{
    /// <summary>
    /// Implicitly converts a <see cref="string"/> to <see cref="MutableString"/>.
    /// </summary>
    public static implicit operator MutableString(string s) => new(s);

    /// <summary>
    /// Implicitly converts a <see cref="MutableString"/> to <see cref="string"/>.
    /// </summary>
    public static implicit operator string(MutableString ms) => ms.ToString();

    /// <summary>
    /// Implicitly converts a <see cref="ReadOnlySpan{T}"/> to <see cref="MutableString"/>.
    /// </summary>
    public static implicit operator MutableString(ReadOnlySpan<char> s) => new(s);

    /// <summary>
    /// Implicitly converts a <see cref="MutableString"/> to <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    public static implicit operator ReadOnlySpan<char>(MutableString ms) => ms.AsSpan();

    /// <summary>
    /// Implicitly converts a char[] to <see cref="MutableString"/>.
    /// </summary>
    public static implicit operator MutableString(char[] array) => new(array);

    /// <summary>
    /// Implicitly converts a <see cref="MutableString"/> to <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    public static implicit operator char[](MutableString ms) => [.. ms];

    #region Append with + operator

    /// <summary>
    /// Implements <c>+</c> operator for two <see cref="MutableString"/>s.
    /// </summary>
    public static MutableString operator +(MutableString? left, MutableString? right)
    {
        MutableString result = new(left);
        result.Append(right);
        return result;
    }

    /// <summary>
    /// Implements <c>+</c> operator for a <see cref="MutableString"/> and <see cref="string"/>.
    /// </summary>
    public static MutableString operator +(MutableString? left, string? right)
    {
        MutableString result = new(left);
        result.Append(right);
        return result;
    }

    /// <summary>
    /// Implements <c>+</c> operator for a <see cref="string"/> and <see cref="MutableString"/>.
    /// </summary>
    public static MutableString operator +(string? left, MutableString? right)
    {
        MutableString result = new(left);
        result.Append(right);
        return result;
    }

    #endregion

    #region Comparison

    /// <summary>
    /// Implements <c>==</c> operator for two <see cref="MutableString"/>s.
    /// </summary>
    public static bool operator ==(MutableString? left, MutableString? right)
    {
        return (left is null) ?
            right is null :
            left.Equals(right);
    }

    /// <summary>
    /// Implements <c>!=</c> operator for two <see cref="MutableString"/>s.
    /// </summary>
    public static bool operator !=(MutableString? left, MutableString? right) => !(left == right);

    /// <summary>
    /// Implements <c>&lt;</c> operator for two <see cref="MutableString"/>s.
    /// </summary>
    public static bool operator <(MutableString? left, MutableString? right)
    {
        return (left is null) ?
            right is not null :
            left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Implements <c>&gt;</c> operator for two <see cref="MutableString"/>s.
    /// </summary>
    public static bool operator >(MutableString? left, MutableString? right) => right < left;

    /// <summary>
    /// Implements <c>&lt;=</c> operator for two <see cref="MutableString"/>s.
    /// </summary>
    public static bool operator <=(MutableString? left, MutableString? right) => !(left > right);

    /// <summary>
    /// Implements <c>&gt;=</c> operator for two <see cref="MutableString"/>s.
    /// </summary>
    public static bool operator >=(MutableString? left, MutableString? right) => !(left < right);

    #endregion

}
