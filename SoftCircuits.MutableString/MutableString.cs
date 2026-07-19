using System.Diagnostics.CodeAnalysis;

namespace SoftCircuits.MutableString;

/// <summary>
/// Represents a mutable string that can be modified without creating new instance.
/// </summary>
public sealed partial class MutableString
{
    /// <summary>
    /// Array to hold the string characters.
    /// </summary>
    private char[] Buffer;

    /// <summary>
    /// The current string length.
    /// </summary>
    private int Count;


    /// <summary>
    /// Constructs an empty <see cref="MutableString"/> instance.
    /// </summary>
    public MutableString()
    {
        Resize(0);
    }

    /// <summary>
    /// Constructs an empty <see cref="MutableString"/> instance.
    /// </summary>
    /// <param name="capacity">The initial capacity of the string.</param>
    public MutableString(int capacity)
    {
#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);
#else
        if (capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));
#endif

        Buffer = new char[capacity];
        Count = 0;
    }

    /// <summary>
    /// Constructs an empty <see cref="MutableString"/> instance.
    /// </summary>
    /// <param name="value">Initial character values.</param>
    public MutableString(MutableString? value)
    {
        if (value == null)
        {
            Resize(0);
        }
        else
        {
            Resize(value.Length);
            Array.Copy(value.Buffer, 0, Buffer, 0, value.Length);
        }
    }

    /// <summary>
    /// Constructs a new <see cref="MutableString"/> instance.
    /// </summary>
    /// <param name="s">Initial character values.</param>
    public MutableString(string? s)
    {
        if (s == null)
        {
            Resize(0);
        }
        else
        {
            Resize(s.Length);
            s.CopyTo(0, Buffer, 0, s.Length);
        }
    }

    /// <summary>
    /// Constructs a new <see cref="MutableString"/> instance.
    /// </summary>
    /// <param name="array">Initial character values.</param>
    public MutableString(char[]? array)
    {
        if (array == null)
        {
            Resize(0);
        }
        else
        {
            Resize(array.Length);
            Array.Copy(array, Buffer, array.Length);
        }
    }

    /// <summary>
    /// Constructs a new <see cref="MutableString"/> instance.
    /// </summary>
    /// <param name="array">Array with initial character values.</param>
    /// <param name="startIndex">Index of starting character to copy.</param>
    /// <param name="length">Number of characters to copy.</param>
    public MutableString(char[]? array, int startIndex, int length)
    {
#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
#else
        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length));
        if (startIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(startIndex));
#endif

        if (array == null)
        {
            Resize(length); // Honors the caller's requested length; zero-filled
        }
        else
        {
            if (startIndex + length > array.Length)
                throw new ArgumentOutOfRangeException(nameof(length));

            Resize(length);
            Array.Copy(array, startIndex, Buffer, 0, length);
        }
    }

    /// <summary>
    /// Constructs a new <see cref="MutableString"/> instance.
    /// </summary>
    /// <param name="span">Initial character values.</param>
    public MutableString(ReadOnlySpan<char> span)
    {
        Resize(span.Length);
        span.CopyTo(Buffer);
    }

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
        Count = length;
    }

    /// <summary>
    /// Returns the current value as a regular <see cref="String"/>.
    /// </summary>
    public override string ToString() => new(Buffer, 0, Count);

    #endregion

    #region Operators

    /// <summary>
    /// Implicitly converts a <see cref="MutableString"/> to a <see cref="String"/>.
    /// </summary>
    /// <param name="ms">The mutable string to convert.</param>
    /// <returns>The converted string.</returns>
    public static implicit operator string(MutableString ms) => ms.ToString();

    /// <summary>
    /// Explicitly converts a <see cref="String"/> to a <see cref="MutableString"/>.
    /// </summary>
    /// <param name="s">The string to convert.</param>
    /// <returns>The converted mutable string.</returns>
    public static explicit operator MutableString(string s) => new(s);

    #endregion
}
