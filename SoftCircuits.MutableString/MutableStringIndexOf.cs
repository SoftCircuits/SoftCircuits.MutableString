namespace SoftCircuits.MutableString;

public sealed partial class MutableString
{
    #region IndexOf

    // --- char overloads ---

    public int IndexOf(char value)
        => IndexOf(value, 0, Count, StringComparison.Ordinal);

    public int IndexOf(char value, int startIndex)
        => IndexOf(value, startIndex, Count - startIndex, StringComparison.Ordinal);

    public int IndexOf(char value, int startIndex, int count)
        => IndexOf(value, startIndex, count, StringComparison.Ordinal);

    public int IndexOf(char value, StringComparison comparisonType)
        => IndexOf(value, 0, Count, comparisonType);

    public int IndexOf(char value, int startIndex, StringComparison comparisonType)
        => IndexOf(value, startIndex, Count - startIndex, comparisonType);

    public int IndexOf(char value, int startIndex, int count, StringComparison comparisonType)
    {
        ReadOnlySpan<char> single = [value];
        return IndexOfCore(single, startIndex, count, comparisonType);
    }

    // --- string overloads ---

    public int IndexOf(string? value)
        => IndexOf(value, 0, Count, StringComparison.Ordinal);

    public int IndexOf(string? value, int startIndex)
        => IndexOf(value, startIndex, Count - startIndex, StringComparison.Ordinal);

    public int IndexOf(string? value, int startIndex, int count)
        => IndexOf(value, startIndex, count, StringComparison.Ordinal);

    public int IndexOf(string? value, StringComparison comparisonType)
        => IndexOf(value, 0, Count, comparisonType);

    public int IndexOf(string? value, int startIndex, StringComparison comparisonType)
        => IndexOf(value, startIndex, Count - startIndex, comparisonType);

    public int IndexOf(string? value, int startIndex, int count, StringComparison comparisonType)
    {
        ArgumentNullException.ThrowIfNull(value);
        return IndexOfCore(value.AsSpan(), startIndex, count, comparisonType);
    }

    // --- MutableString overloads ---

    public int IndexOf(MutableString? value)
        => IndexOf(value, 0, Count, StringComparison.Ordinal);

    public int IndexOf(MutableString? value, int startIndex)
        => IndexOf(value, startIndex, Count - startIndex, StringComparison.Ordinal);

    public int IndexOf(MutableString? value, int startIndex, int count)
        => IndexOf(value, startIndex, count, StringComparison.Ordinal);

    public int IndexOf(MutableString? value, StringComparison comparisonType)
        => IndexOf(value, 0, Count, comparisonType);

    public int IndexOf(MutableString? value, int startIndex, StringComparison comparisonType)
        => IndexOf(value, startIndex, Count - startIndex, comparisonType);

    public int IndexOf(MutableString? value, int startIndex, int count, StringComparison comparisonType)
    {
        ArgumentNullException.ThrowIfNull(value);
        return IndexOfCore(value.AsSpan(), startIndex, count, comparisonType);
    }

    // --- shared core ---

    private int IndexOfCore(ReadOnlySpan<char> value, int startIndex, int count, StringComparison comparisonType)
    {
#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, Count);
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(count, Count - startIndex);
#else
        if (startIndex < 0 || startIndex > Count)
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (count < 0 || count > Count - startIndex)
            throw new ArgumentOutOfRangeException(nameof(count));
#endif

        int found = AsSpan().Slice(startIndex, count).IndexOf(value, comparisonType);
        return found < 0 ? -1 : found + startIndex;
    }

    #endregion

    #region Contains

    public bool Contains(char value) => IndexOf(value) >= 0;
    public bool Contains(char value, StringComparison comparisonType) => IndexOf(value, comparisonType) >= 0;
    public bool Contains(string? value) => IndexOf(value) >= 0;
    public bool Contains(string? value, StringComparison comparisonType) => IndexOf(value, comparisonType) >= 0;
    public bool Contains(MutableString? value) => IndexOf(value) >= 0;
    public bool Contains(MutableString? value, StringComparison comparisonType) => IndexOf(value, comparisonType) >= 0;

    #endregion

    #region StartsWith / EndsWith

    public bool StartsWith(char value) => Count > 0 && Buffer[0] == value;

    public bool StartsWith(string? value, StringComparison comparisonType = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(value);
        return AsSpan().StartsWith(value.AsSpan(), comparisonType);
    }

    public bool StartsWith(MutableString? value, StringComparison comparisonType = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(value);
        return AsSpan().StartsWith(value.AsSpan(), comparisonType);
    }

    public bool EndsWith(char value) => Count > 0 && Buffer[Count - 1] == value;

    public bool EndsWith(string? value, StringComparison comparisonType = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(value);
        return AsSpan().EndsWith(value.AsSpan(), comparisonType);
    }

    public bool EndsWith(MutableString? value, StringComparison comparisonType = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(value);
        return AsSpan().EndsWith(value.AsSpan(), comparisonType);
    }

    #endregion

    #region Split

    public string[] Split(char separator, StringSplitOptions options = StringSplitOptions.None)
        => ToString().Split(separator, options);

    public string[] Split(string? separator, StringSplitOptions options = StringSplitOptions.None)
        => ToString().Split(separator, options);

    public string[] Split(params char[]? separator)
        => ToString().Split(separator);

    #endregion

}
