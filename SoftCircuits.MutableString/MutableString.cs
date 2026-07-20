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
    private int Count;

    /// <summary>
    /// Returns the current value as a regular <see cref="String"/>.
    /// </summary>
    public override string ToString() => new(Buffer, 0, Count);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public string ToString(int startIndex)
    {
        if (startIndex < 0 || startIndex >= Count)
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        return new string(Buffer, startIndex, Count - startIndex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public string ToString(int startIndex, int length)
    {
        if (startIndex < 0 || startIndex >= Count)
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (length < 0 || startIndex + length > Count)
            throw new ArgumentOutOfRangeException(nameof(length));
        return new string(Buffer, startIndex, length);
    }

    /// <summary>
    /// Creates a new span from this <see cref="MutableString"/> instance. The span is
    /// only valid until the next modification of this instance.
    /// </summary>
    /// <returns></returns>
    public ReadOnlySpan<char> AsSpan() => new(Buffer, 0, Count);

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
        Count = length;
        EnumeratorVersion++;
    }

    #endregion

}
