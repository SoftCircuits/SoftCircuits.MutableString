/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString;

public sealed partial class MutableString
{
    /// <summary>
    /// Trims leading whitespace off this <see cref="MutableString"/> instance.
    /// </summary>
    public void TrimStart()
    {
        int i = 0;
        while (i < InternalLength && char.IsWhiteSpace(Buffer[i]))
            i++;

        if (i > 0)
            Remove(0, i);
    }

    /// <summary>
    /// Trims trailing whitespace off this <see cref="MutableString"/> instance.
    /// </summary>
    public void TrimEnd()
    {
        int i = InternalLength;
        while (i > 0 && char.IsWhiteSpace(Buffer[i - 1]))
            i--;

        if (i < InternalLength)
            Remove(i, InternalLength - i);
    }

    /// <summary>
    /// Trims leading and trailing whitespace off this <see cref="MutableString"/> instance.
    /// </summary>
    public void Trim()
    {
        TrimEnd();
        TrimStart();
    }
}
