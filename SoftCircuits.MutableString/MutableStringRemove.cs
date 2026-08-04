/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString;

public sealed partial class MutableString
{
    /// <summary>
    /// Deletes the specified number of characters at the specified index.
    /// </summary>
    /// <param name="index">The starting index where characters should be deleted.</param>
    /// <param name="count">The number of characters to delete.</param>
    public void Remove(int index, int count)
    {
        int oldLength = InternalLength;
        if (index < 0 || index >= oldLength || count <= 0)
            return;

        int maxCount = oldLength - index;
        if (count > maxCount)
            count = maxCount;

        // Shift characters
        if (index + count < oldLength)
            Copy(index + count, index, oldLength - index - count);

        // Resize array
        Resize(oldLength - count);
    }

    /// <summary>
    /// Deletes all characters starting at the specified index until the end
    /// of the string.
    /// </summary>
    /// <param name="index">The starting index where characters should be deleted.</param>
    public void Remove(int index) => Remove(index, InternalLength - index);
}
