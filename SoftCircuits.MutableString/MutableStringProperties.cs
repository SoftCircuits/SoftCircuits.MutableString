/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString;

public sealed partial class MutableString
{
    /// <summary>
    /// Gets or sets the length of this <see cref="MutableString"/> object. Setting this property will
    /// resize the string.
    /// </summary>
    public int Length
    {
        get => InternalLength;
        set => Resize(value);
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="MutableString"/> object contains no characters.
    /// </summary>
    public bool IsEmpty => InternalLength == 0;

    /// <summary>
    /// Gets a value indicating whether this <see cref="MutableString"/> object consists only of whitespace characters.
    /// Returns true also if the string is empty.
    /// </summary>
    public bool IsWhiteSpace
    {
        get
        {
            for (int i = 0; i < InternalLength; i++)
            {
                if (!char.IsWhiteSpace(Buffer[i]))
                    return false;
            }
            return true;
        }
    }

    /// <summary>
    /// Gets or sets the character at the specified index.
    /// </summary>
    public char this[int index]
    {
        get
        {
            if (index < 0 || index >= InternalLength)
                throw new IndexOutOfRangeException(nameof(index));
            return Buffer[index];
        }
        set
        {
            if (index < 0 || index >= InternalLength)
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
            if (index.Value < 0 || index.Value >= InternalLength)
                throw new IndexOutOfRangeException(nameof(index));
            return Buffer[index.GetOffset(InternalLength)];
        }
        set
        {
            if (index.Value < 0 || index.Value >= InternalLength)
                throw new IndexOutOfRangeException(nameof(index));
            Buffer[index.GetOffset(InternalLength)] = value;
        }
    }

    /// <summary>
    /// Returns the specified range of this string.
    /// </summary>
    public string this[Range range]
    {
        get
        {
            (int offset, int length) = range.GetOffsetAndLength(InternalLength);
            return new string(Buffer, offset, length);
        }
    }
}
