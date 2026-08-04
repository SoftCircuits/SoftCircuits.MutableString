/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString;

public sealed partial class MutableString
{
    /// <summary>
    /// Right aligns the characters in this string by padding them with the specified padding character on the left,
    /// for the specified total length.
    /// </summary>
    /// <param name="totalWidth">The number of characters in the resulting string, after the padding characters are applied.</param>
    /// <param name="paddingChar">The character to insert for padding.</param>
    public void PadLeft(int totalWidth, char paddingChar)
    {
        int padCount = totalWidth - InternalLength;
        if (padCount <= 0)
            return;
        Insert(0, paddingChar, padCount);
    }

    /// <summary>
    /// Right aligns the characters in this string by padding them with spaces on the left,
    /// for the specified total length.
    /// </summary>
    /// <param name="totalWidth">The number of characters in the resulting string, after the padding characters are applied.</param>
    public void PadLeft(int totalWidth) => PadLeft(totalWidth, ' ');

    /// <summary>
    /// Left aligns the characters in this string by padding them with the specified padding character on the right,
    /// for the specified total length.
    /// </summary>
    /// <param name="totalWidth">The number of characters in the resulting string, after the padding characters are applied.</param>
    /// <param name="paddingChar">The character to insert for padding.</param>
    public void PadRight(int totalWidth, char paddingChar)
    {
        int padCount = totalWidth - InternalLength;
        if (padCount <= 0)
            return;
        Insert(InternalLength, paddingChar, padCount);
    }

    /// <summary>
    /// Left aligns the characters in this string by padding them with spaces on the right,
    /// for the specified total length.
    /// </summary>
    /// <param name="totalWidth">The number of characters in the resulting string, after the padding characters are applied.</param>
    public void PadRight(int totalWidth) => PadRight(totalWidth, ' ');
}
