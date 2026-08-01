/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString;

public sealed partial class MutableString
{
    /// <summary>
    /// Right aligns the characters in this string by padding them with spaces on the left,
    /// for the specified total length.
    /// </summary>
    /// <param name="totalWidth"></param>
    /// <param name="paddingChar"></param>
    public void PadLeft(int totalWidth, char paddingChar)
    {
        int padCount = totalWidth - Count;
        if (padCount <= 0)
            return;
        Insert(0, paddingChar, padCount);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="totalWidth"></param>
    public void PadLeft(int totalWidth) => PadLeft(totalWidth, ' ');

    /// <summary>
    /// 
    /// </summary>
    /// <param name="totalWidth"></param>
    /// <param name="paddingChar"></param>
    public void PadRight(int totalWidth, char paddingChar)
    {
        int padCount = totalWidth - Count;
        if (padCount <= 0)
            return;
        Insert(Count, paddingChar, padCount);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="totalWidth"></param>
    public void PadRight(int totalWidth) => PadRight(totalWidth, ' ');
}
