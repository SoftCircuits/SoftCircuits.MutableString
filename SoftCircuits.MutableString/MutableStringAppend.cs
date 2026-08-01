/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

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
}
