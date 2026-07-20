using System.Diagnostics;

namespace SoftCircuits.MutableString;

public sealed partial class MutableString
{
    /// <summary>
    /// Appends the specified <see cref="string"/> to this object.
    /// </summary>
    /// <param name="s">The string to append.</param>
    public void Append(string? s) => Append(s.AsSpan());

    /// <summary>
    /// Appends the specified <see cref="MutableString"/> to this object.
    /// </summary>
    /// <param name="value">The MutableString to append.</param>
    public void Append(MutableString? value)
    {
        if (value != null)
            Append(value.Buffer.AsSpan(0, value.Count));
    }

    /// <summary>
    /// Appends the specified char array to this object.
    /// </summary>
    /// <param name="array">The char array to append.</param>
    public void Append(char[]? array) => Append(array.AsSpan());

    /// <summary>
    /// Appends the specified <see cref="ReadOnlySpan{T}"/> to this object.
    /// </summary>
    /// <param name="span">The span to append.</param>
    public void Append(ReadOnlySpan<char> span)
    {
        if (span.Length == 0)
            return;

        // Resize array
        int oldLength = Count;
        Resize(oldLength + span.Length);

        // Copy string
        Copy(span, oldLength);
    }

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
        if (span.Length == 0 || index < 0)
            return;

        // Ensure valid index
        int oldLength = Count;
        if (index > oldLength)
            index = oldLength;

        // Resize array
        Resize(oldLength + span.Length);

        // Shift characters to make room
        if (index < oldLength)
            Move(index, index + span.Length, oldLength - index);

        // Copy string
        Copy(span, index);
    }

    /// <summary>
    /// Inserts the specified <see cref="string"/>  at the specified index, replacing the specified number of characters.
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
    /// Inserts the specified <see cref="MutableString"/>  at the specified index, replacing the specified number of characters.
    /// <paramref name="replaceCount"/> can be less than or greater than the length of <paramref name="value"/>.
    /// </summary>
    /// <param name="index">The index where the string should be inserted.</param>
    /// <param name="value">The string to insert.</param>
    /// <param name="replaceCount">The number of characters to replace.</param>
    public void Replace(int index, MutableString? value, int replaceCount)
    {
        if (value == null)
        {
            Delete(index, replaceCount);
        }
        else
        {
            Replace(index, value.Buffer.AsSpan(0, value.Count), replaceCount);
        }
    }

    /// <summary>
    /// Inserts the specified <see cref="ReadOnlySpan{Char}"/>  at the specified index, replacing the specified number of characters.
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
            Delete(index, replaceCount);
            return;
        }

        // Ensure valid index
        int oldLength = Count;
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
                Move(offset, offset + delta, oldLength - offset);
            }
        }
        else if (delta < 0)
        {
            // Shift characters
            if (index + replaceCount < oldLength)
            {
                int offset = index + replaceCount;
                Move(offset, offset + delta, oldLength - offset);
            }

            // Shrink array
            Resize(oldLength + delta);
        }

        // Copy string
        Copy(span, index);
    }

    /// <summary>
    /// Deletes the specified number of characters at the specified index.
    /// </summary>
    /// <param name="index">The starting index where characters should be deleted.</param>
    /// <param name="count">The number of characters to delete.</param>
    public void Delete(int index, int count)
    {
        int oldLength = Count;
        if (index >= oldLength || count <= 0 || index < 0)
            return;

        int maxCount = oldLength - index;
        if (count > maxCount)
            count = maxCount;

        // Shift characters
        if (index + count < oldLength)
            Move(index + count, index, oldLength - index - count);

        // Resize array
        Resize(oldLength - count);
    }

    /// <summary>
    /// Copies characters from one part of the array to another.
    /// </summary>
    /// <param name="sourceIndex">Starting index of where characters are copied from.</param>
    /// <param name="targetIndex">Starting index of where characters are copied to.</param>
    /// <param name="count">The number of characters to copy.</param>
    public void Move(int sourceIndex, int targetIndex, int count)
    {
        int maxCount = Count - Math.Max(sourceIndex, targetIndex);
        if (count > maxCount)
            count = maxCount;

        if (count <= 0)
            return;

        // Copy characters
        Array.Copy(Buffer, sourceIndex, Buffer, targetIndex, count);
    }

    /// <summary>
    /// Copies the given string to this <see cref="MutableString"/> object at the specified index.
    /// Does not grow the string. Any characters that would be copied beyond the end of the string
    /// are ignored.
    /// </summary>
    /// <param name="s">The string to copy.</param>
    /// <param name="targetIndex">The starting index where the string should be copied.</param>
    public void Copy(string? s, int targetIndex) => Copy(s.AsSpan(), targetIndex);

    /// <summary>
    /// Copies the given <see cref="MutableString"/> to this MutableString object at the specified index.
    /// Does not grow the string. Any characters that would be copied beyond the end of the string
    /// are ignored.
    /// </summary>
    /// <param name="value">The value to copy.</param>
    /// <param name="targetIndex">The starting index where the value should be copied.</param>
    public void Copy(MutableString? value, int targetIndex)
    {
        if (value != null)
            Copy(value.Buffer.AsSpan(0, value.Count), targetIndex);
    }

    /// <summary>
    /// Copies the given char array to this <see cref="MutableString"/> object at the specified index.
    /// Does not grow the string. Any characters that would be copied beyond the end of the string
    /// are ignored.
    /// </summary>
    /// <param name="array">The char array to copy.</param>
    /// <param name="targetIndex">The starting index where the array should be copied.</param>
    public void Copy(char[]? array, int targetIndex) => Copy(array.AsSpan(), targetIndex);

    /// <summary>
    /// Copies the given <see cref="ReadOnlySpan{Char}"/> to this <see cref="MutableString"/> object at the specified index.
    /// Does not grow the string. Any characters that would be copied beyond the end of the string
    /// are ignored.
    /// </summary>
    /// <param name="span">The span to copy.</param>
    /// <param name="targetIndex">The starting index where the span should be copied.</param>
    public void Copy(ReadOnlySpan<char> span, int targetIndex)
    {
        int count = span.Length;
        if (count > Count - targetIndex)
            count = Count - targetIndex;

        if (count <= 0)
            return;

        Debug.Assert(targetIndex + count <= Count);

        // Copy characters
        span[..count].CopyTo(Buffer.AsSpan(targetIndex, count));
    }



















    public MutableString TrimStart()
    {
        int i = 0;
        while (i < Count && char.IsWhiteSpace(Buffer[i]))
            i++;

        if (i > 0)
            Delete(0, i);

        return this;
    }

    public MutableString TrimEnd()
    {
        int i = Count;
        while (i > 0 && char.IsWhiteSpace(Buffer[i - 1]))
            i--;

        if (i < Count)
            Delete(i, Count - i);

        return this;
    }

    public MutableString Trim()
    {
        TrimEnd();
        TrimStart();
        return this;
    }

}
