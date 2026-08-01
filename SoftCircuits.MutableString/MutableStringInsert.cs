/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString;

public sealed partial class MutableString
{
    /// <summary>
    /// Inserts the specified string at the specified index.
    /// </summary>
    /// <param name="index">The index where the string should be inserted.</param>
    /// <param name="s">The string to insert.</param>
    public void Insert(int index, string? s) => Insert(index, s.AsSpan());

    /// <summary>
    /// Inserts the specified <see cref="MutableString"/> at the specified index.
    /// </summary>
    /// <param name="index">The index where the string should be inserted.</param>
    /// <param name="value">The string to insert.</param>
    public void Insert(int index, MutableString? value)
    {
        if (value != null)
            Insert(index, value.Buffer.AsSpan(0, value.Count));
    }

    /// <summary>
    /// Inserts the specified char array at the specified index.
    /// </summary>
    /// <param name="index">The index where the string should be inserted.</param>
    /// <param name="array">The char array to insert.</param>
    public void Insert(int index, char[]? array) => Insert(index, array.AsSpan());

    /// <summary>
    /// Inserts the specified <see cref="ReadOnlySpan{T}"/> at the specified index.
    /// </summary>
    /// <param name="index">The index where the string should be inserted.</param>
    /// <param name="span">The span to insert.</param>
    public void Insert(int index, ReadOnlySpan<char> span)
    {
        if (index < 0 || span.Length == 0)
            return;

        // Ensure valid index
        int oldLength = Count;
        if (index > oldLength)
            index = oldLength;

        // Resize array
        Resize(oldLength + span.Length);

        // Shift characters to make room
        if (index < oldLength)
            Copy(index, index + span.Length, oldLength - index);

        // Copy string
        Copy(span, index);
    }

    /// <summary>
    /// Inserts the specified character at the specified index repeated
    /// specified number of times.
    /// </summary>
    /// <param name="index">The index where the characters should be inserted.</param>
    /// <param name="c">The character to insert.</param>
    /// <param name="count">The number of times the character should be inserted.</param>
    public void Insert(int index, char c, int count)
    {
        if (index < 0 || count <= 0)
            return;

        // Ensure valid index
        int oldLength = Count;
        if (index > oldLength)
            index = oldLength;

        // Resize array
        Resize(oldLength + count);

        // Shift characters to make room
        if (index < oldLength)
            Copy(index, index + count, oldLength - index);

        // Insert characters
        for (int i = 0; i < count; i++)
            Buffer[index + i] = c;
    }
}
