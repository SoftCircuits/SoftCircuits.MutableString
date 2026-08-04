/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString;

public sealed partial class MutableString
{
    /// <summary>
    /// Inserts the specified <see cref="string"/> at the specified index, replacing the characters at that
    /// index.
    /// </summary>
    /// <param name="index">The index where the string should be inserted.</param>
    /// <param name="s">The string to insert.</param>
    public void Replace(int index, string? s)
    {
        if (s != null)
            Replace(index, s.AsSpan(), s.Length);
    }

    /// <summary>
    /// Inserts the specified char array at the specified index, replacing the characters at that
    /// index.
    /// </summary>
    /// <param name="index">The index where the char array should be inserted.</param>
    /// <param name="array">The char array to insert.</param>
    public void Replace(int index, char[]? array)
    {
        if (array != null)
            Replace(index, array.AsSpan(), array.Length);
    }

    /// <summary>
    /// Inserts the specified <see cref="MutableString"/> at the specified index, replacing the characters at that
    /// index.
    /// </summary>
    /// <param name="index">The index where the string should be inserted.</param>
    /// <param name="value">The string to insert.</param>
    public void Replace(int index, MutableString? value)
    {
        if (value != null)
            Replace(index, value.Buffer.AsSpan(0, value.InternalLength), value.Length);
    }

    /// <summary>
    /// Inserts the specified <see cref="string"/> at the specified index, replacing the specified number of characters.
    /// <paramref name="replaceCount"/> can be less than or greater than the length of <paramref name="s"/>.
    /// </summary>
    /// <param name="index">The index where the string should be inserted.</param>
    /// <param name="s">The string to insert.</param>
    /// <param name="replaceCount">The number of characters to replace.</param>
    public void Replace(int index, string? s, int replaceCount) => Replace(index, s.AsSpan(), replaceCount);

    /// <summary>
    /// Inserts the specified char array at the specified index, replacing the specified number of characters.
    /// <paramref name="replaceCount"/> can be less than or greater than the length of <paramref name="array"/>.
    /// </summary>
    /// <param name="index">The index where the char array should be inserted.</param>
    /// <param name="array">The char array to insert.</param>
    /// <param name="replaceCount">The number of characters to replace.</param>
    public void Replace(int index, char[]? array, int replaceCount) => Replace(index, array.AsSpan(), replaceCount);

    /// <summary>
    /// Inserts the specified <see cref="MutableString"/> at the specified index, replacing the specified number of characters.
    /// <paramref name="replaceCount"/> can be less than or greater than the length of <paramref name="value"/>.
    /// </summary>
    /// <param name="index">The index where the string should be inserted.</param>
    /// <param name="value">The string to insert.</param>
    /// <param name="replaceCount">The number of characters to replace.</param>
    public void Replace(int index, MutableString? value, int replaceCount)
    {
        if (value == null)
        {
            Remove(index, replaceCount);
        }
        else
        {
            Replace(index, value.Buffer.AsSpan(0, value.InternalLength), replaceCount);
        }
    }

    /// <summary>
    /// Inserts the specified <see cref="ReadOnlySpan{Char}"/> at the specified index, replacing the specified number of characters.
    /// <paramref name="replaceCount"/> can be less than or greater than the length of <paramref name="span"/>.
    /// </summary>
    /// <param name="index">The index where the string should be inserted.</param>
    /// <param name="span">The string to insert.</param>
    /// <param name="replaceCount">The number of characters to replace.</param>
    public void Replace(int index, ReadOnlySpan<char> span, int replaceCount)
    {
        if (index < 0)
            return;

        if (span.Length == 0)
        {
            Remove(index, replaceCount);
            return;
        }

        // Ensure valid index
        int oldLength = InternalLength;
        if (index > oldLength)
            index = oldLength;

        // Ensure valid replacement character count
        if (replaceCount < 0)
            replaceCount = 0;
        else if (replaceCount > oldLength - index)
            replaceCount = oldLength - index;

        // Determine if string grows or shrinks
        int delta = span.Length - replaceCount;
        if (delta > 0)
        {
            // Grow array
            Resize(oldLength + delta);

            // Shift characters
            if (index + replaceCount < oldLength)
            {
                int offset = index + replaceCount;
                Copy(offset, offset + delta, oldLength - offset);
            }
        }
        else if (delta < 0)
        {
            // Shift characters
            if (index + replaceCount < oldLength)
            {
                int offset = index + replaceCount;
                Copy(offset, offset + delta, oldLength - offset);
            }

            // Shrink array
            Resize(oldLength + delta);
        }

        // Copy string
        Copy(span, index);
    }

    #region String Replacement

    /// <summary>
    /// Replaces all occurrences of <paramref name="oldChar"/> with <paramref name="newChar"/>.
    /// </summary>
    /// <param name="oldChar">The character to be replaced.</param>
    /// <param name="newChar">The new character.</param>
    public void Replace(char oldChar, char newChar)
    {
        for (int i = 0; i < InternalLength; i++)
        {
            if (Buffer[i] == oldChar)
                Buffer[i] = newChar;
        }
    }

    /// <summary>
    /// Replaces all occurrences of <paramref name="oldValue"/> with <paramref name="newValue"/>.
    /// </summary>
    /// <param name="oldValue">The string to be replaced.</param>
    /// <param name="newValue">The new string.</param>
    public void Replace(string oldValue, string? newValue) => Replace(oldValue, newValue, StringComparison.Ordinal);

    /// <summary>
    /// Replaces all occurrences of <paramref name="oldValue"/> with <paramref name="newValue"/>.
    /// </summary>
    /// <param name="oldValue">The string to be replaced.</param>
    /// <param name="newValue">The new string.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public void Replace(string oldValue, string? newValue, StringComparison comparisonType)
    {
        newValue ??= string.Empty;

        int i = 0;
        while (true)
        {
            i = IndexOf(oldValue, i, comparisonType);
            if (i < 0)
                break;
            Replace(i, newValue, oldValue.Length);
            i += newValue.Length;
        }
    }

    #endregion

}
