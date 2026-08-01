/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

using System.Diagnostics;

namespace SoftCircuits.MutableString;

public sealed partial class MutableString
{
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

    /// <summary>
    /// Copies characters from one part of the array to another.
    /// </summary>
    /// <param name="sourceIndex">Starting index of where characters are copied from.</param>
    /// <param name="targetIndex">Starting index of where characters are copied to.</param>
    /// <param name="count">The number of characters to copy.</param>
    public void Copy(int sourceIndex, int targetIndex, int count)
    {
        int maxCount = Count - Math.Max(sourceIndex, targetIndex);
        if (count > maxCount)
            count = maxCount;

        if (count <= 0)
            return;

        // Copy characters
        Array.Copy(Buffer, sourceIndex, Buffer, targetIndex, count);
    }
}
