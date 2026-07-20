namespace SoftCircuits.MutableString;

public sealed partial class MutableString
{

    public void CopyTo(char[] destination, int index, int count)
    {
        //if (count > this.length)
        //{
        //    ThrowHelper.ThrowArgumentOutOfRangeException(nameof(count), "Number of copying characters is greater than the string length.");
        //}

        Array.Copy(Buffer, 0, destination, index, count);
    }

    public void CopyTo(Span<char> destination)
    {
        AsSpan().CopyTo(destination);
    }

    /// <summary>
    /// Gets or sets the character at the specified index.
    /// </summary>
    public char this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException(nameof(index));
            return Buffer[index];
        }
        set
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException(nameof(index));
            Buffer[index] = value;
        }
    }

    /// <summary>
    /// Gets or sets the character at the specified index.
    /// </summary>
    public char this[Index index]
    {
        get
        {
            if (index.Value < 0 || index.Value >= Count)
                throw new IndexOutOfRangeException(nameof(index));
            return Buffer[index.GetOffset(Count)];
        }
        set
        {
            if (index.Value < 0 || index.Value >= Count)
                throw new IndexOutOfRangeException(nameof(index));
            Buffer[index.GetOffset(Count)] = value;
        }
    }

    /// <summary>
    /// Returns the specified range of this string.
    /// </summary>
    public string this[Range range]
    {
        get
        {
            (int offset, int length) = range.GetOffsetAndLength(Count);
            return new string(Buffer, offset, length);
        }
    }

    /// <summary>
    /// Gets or sets the length of this <see cref="MutableString"/> object. Setting this property will
    /// resize the string.
    /// </summary>
    public int Length
    {
        get => Count;
        set => Resize(value);
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="MutableString"/> object is empty.
    /// </summary>
    public bool IsEmpty => Count == 0;

    /// <summary>
    /// Gets a value indicating whether this <see cref="MutableString"/> object consists only of whitespace characters.
    /// Returns true also if the string is empty.
    /// </summary>
    public bool IsWhiteSpace
    {
        get
        {
            for (int i = 0; i < Count; i++)
            {
                if (!char.IsWhiteSpace(Buffer[i]))
                    return false;
            }
            return true;
        }
    }
}
