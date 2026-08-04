/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

using System.Diagnostics.CodeAnalysis;

namespace SoftCircuits.MutableString;

/// <summary>
/// Represents a mutable string that can be modified without creating new instance.
/// </summary>
public sealed partial class MutableString : ICloneable
{
    /// <summary>
    /// Array to hold the string characters.
    /// </summary>
    private char[] Buffer;

    /// <summary>
    /// The current string length.
    /// </summary>
    /// <remarks>
    /// Only ever modified within <see cref="Resize"/>. That method also bumps
    /// <see cref="EnumeratorVersion"/>, which the enumerator relies on to detect
    /// concurrent modification. Do not assign this field elsewhere.
    /// </remarks>
    private int InternalLength;

    /// <summary>
    /// Converts this <see cref="MutableString"/> instance to a <see cref="string"/>.
    /// </summary>
    public override string ToString() => new(Buffer, 0, InternalLength);

    /// <summary>
    /// Converts this <see cref="MutableString"/> instance to a <see cref="string"/>.
    /// </summary>
    /// <param name="startIndex">Index of starting character to return</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public string ToString(int startIndex)
    {
        if (startIndex < 0 || startIndex >= InternalLength)
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        return new string(Buffer, startIndex, InternalLength - startIndex);
    }

    /// <summary>
    /// Converts this <see cref="MutableString"/> instance to a <see cref="string"/>.
    /// </summary>
    /// <param name="startIndex">Index of starting character to return</param>
    /// <param name="length">Number of characters to return.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public string ToString(int startIndex, int length)
    {
        if (startIndex < 0 || startIndex >= InternalLength)
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (length < 0 || startIndex + length > InternalLength)
            throw new ArgumentOutOfRangeException(nameof(length));
        return new string(Buffer, startIndex, length);
    }

    /// <summary>
    /// Creates a new <see cref="ReadOnlySpan{T}"/> from this <see cref="MutableString"/>
    /// instance. The span is only valid until the next modification of this instance.
    /// </summary>
    public ReadOnlySpan<char> AsSpan() => new(Buffer, 0, InternalLength);

    /// <summary>
    /// Copies the contents of this <see cref="MutableString"/> to the specified
    /// span.
    /// </summary>
    /// <param name="destination">The span to copy characters into.</param>
    public void CopyTo(Span<char> destination)
    {
        AsSpan().CopyTo(destination);
    }

    /// <summary>
    /// Copies the contents of this <see cref="MutableString"/> to the specified
    /// array.
    /// </summary>
    /// <param name="destination">The span to copy characters into.</param>
    /// <param name="index">The target index to copy characters.</param>
    /// <param name="count">The number of characters to copy.</param>
    public void CopyTo(char[] destination, int index, int count)
    {
#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfGreaterThan(count, InternalLength);
#else
        if (count > InternalLength)
            throw new ArgumentOutOfRangeException(nameof(count));
#endif
        Array.Copy(Buffer, 0, destination, index, count);
    }

    #region ICloneable

    /// <summary>
    /// Returns a copy of this <see cref="MutableString"/> instance.
    /// </summary>
    public object Clone() => new MutableString(this);

    #endregion

    #region Primitives

    /// <summary>
    /// Resizes this <see cref="MutableString"/> object.
    /// </summary>
    /// <param name="length">Specifies the new string length.</param>
    [MemberNotNull(nameof(Buffer))]
    private void Resize(int length)
    {
        if (length < 0)
            length = 0;

        if (Buffer == null || Buffer.Length < length)
        {
            // To minimize the number of reallocations, double requested size
            Array.Resize(ref Buffer, Math.Max(length * 2, 32));
        }
        InternalLength = length;
        EnumeratorVersion++;
    }

    #endregion

}
