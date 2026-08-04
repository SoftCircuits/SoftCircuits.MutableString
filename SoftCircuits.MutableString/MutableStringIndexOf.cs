/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString;

public sealed partial class MutableString
{
    #region IndexOf

    // --- char overloads ---

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    public int IndexOf(char value)
        => IndexOf(value, 0, InternalLength, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    public int IndexOf(char value, int startIndex)
        => IndexOf(value, startIndex, InternalLength - startIndex, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    /// <param name="count">The number of characters to compare.</param>
    public int IndexOf(char value, int startIndex, int count)
        => IndexOf(value, startIndex, count, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int IndexOf(char value, StringComparison comparisonType)
        => IndexOf(value, 0, InternalLength, comparisonType);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int IndexOf(char value, int startIndex, StringComparison comparisonType)
        => IndexOf(value, startIndex, InternalLength - startIndex, comparisonType);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    /// <param name="count">The number of characters to compare.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int IndexOf(char value, int startIndex, int count, StringComparison comparisonType)
    {
        ReadOnlySpan<char> single = [value];
        return IndexOfCore(single, startIndex, count, comparisonType);
    }

    // --- string overloads ---

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    public int IndexOf(string? value)
        => IndexOf(value, 0, InternalLength, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    public int IndexOf(string? value, int startIndex)
        => IndexOf(value, startIndex, InternalLength - startIndex, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    /// <param name="count">The number of characters to compare.</param>
    public int IndexOf(string? value, int startIndex, int count)
        => IndexOf(value, startIndex, count, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int IndexOf(string? value, StringComparison comparisonType)
        => IndexOf(value, 0, InternalLength, comparisonType);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int IndexOf(string? value, int startIndex, StringComparison comparisonType)
        => IndexOf(value, startIndex, InternalLength - startIndex, comparisonType);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    /// <param name="count">The number of characters to compare.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int IndexOf(string? value, int startIndex, int count, StringComparison comparisonType)
    {
        ArgumentNullException.ThrowIfNull(value);
        return IndexOfCore(value.AsSpan(), startIndex, count, comparisonType);
    }

    // --- MutableString overloads ---

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    public int IndexOf(MutableString? value)
        => IndexOf(value, 0, InternalLength, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    public int IndexOf(MutableString? value, int startIndex)
        => IndexOf(value, startIndex, InternalLength - startIndex, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    /// <param name="count">The number of characters to compare.</param>
    public int IndexOf(MutableString? value, int startIndex, int count)
        => IndexOf(value, startIndex, count, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int IndexOf(MutableString? value, StringComparison comparisonType)
        => IndexOf(value, 0, InternalLength, comparisonType);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int IndexOf(MutableString? value, int startIndex, StringComparison comparisonType)
        => IndexOf(value, startIndex, InternalLength - startIndex, comparisonType);

    /// <summary>
    /// Returns the zero-based index of the first occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    /// <param name="count">The number of characters to compare.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int IndexOf(MutableString? value, int startIndex, int count, StringComparison comparisonType)
    {
        ArgumentNullException.ThrowIfNull(value);
        return IndexOfCore(value.AsSpan(), startIndex, count, comparisonType);
    }

    // --- shared core ---

    // Internal IndexOf
    private int IndexOfCore(ReadOnlySpan<char> value, int startIndex, int count, StringComparison comparisonType)
    {
#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, InternalLength);
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(count, InternalLength - startIndex);
#else
        if (startIndex < 0 || startIndex > InternalLength)
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (count < 0 || count > InternalLength - startIndex)
            throw new ArgumentOutOfRangeException(nameof(count));
#endif

        int found = AsSpan().Slice(startIndex, count).IndexOf(value, comparisonType);
        return found < 0 ? -1 : found + startIndex;
    }

    #endregion

    #region IndexOfAny

    /// <summary>
    /// Returns the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode
    /// characters.
    /// </summary>
    /// <param name="anyOf">An array of Unicode characters to find.</param>
    /// <returns>The zero-based index of the first occurrence of any character in this instance, or -1 if not found.</returns>
    public int IndexOfAny(char[] anyOf)
        => AsSpan().IndexOfAny(anyOf);

    /// <summary>
    /// Returns the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode
    /// characters, starting at a specified character position.
    /// </summary>
    /// <param name="anyOf">An array of Unicode characters to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    /// <returns>The zero-based index of the first occurrence of any character in this instance, or -1 if not found.</returns>
    public int IndexOfAny(char[] anyOf, int startIndex)
        => AsSpan()[startIndex..].IndexOfAny(anyOf);

    /// <summary>
    /// Returns the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode
    /// characters, examining a specified number of characters.
    /// </summary>
    /// <param name="anyOf">An array of Unicode characters to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    /// <param name="count">The number of characters to examine.</param>
    /// <returns>The zero-based index of the first occurrence of any character in this instance, or -1 if not found.</returns>
    public int IndexOfAny(char[] anyOf, int startIndex, int count)
        => AsSpan().Slice(startIndex, count).IndexOfAny(anyOf);

    #endregion

    #region LastIndexOf

    // --- char overloads ---

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    public int LastIndexOf(char value)
        => LastIndexOf(value, InternalLength == 0 ? 0 : InternalLength - 1, InternalLength, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value, searching
    /// backward from <paramref name="startIndex"/> to the beginning of the string.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the backward search.</param>
    public int LastIndexOf(char value, int startIndex)
        => LastIndexOf(value, startIndex, startIndex + 1, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value, examining
    /// <paramref name="count"/> characters ending at (and including) <paramref name="startIndex"/>.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the backward search.</param>
    /// <param name="count">The number of characters to examine, counting backward from <paramref name="startIndex"/>.</param>
    public int LastIndexOf(char value, int startIndex, int count)
        => LastIndexOf(value, startIndex, count, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int LastIndexOf(char value, StringComparison comparisonType)
        => LastIndexOf(value, InternalLength == 0 ? 0 : InternalLength - 1, InternalLength, comparisonType);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value, searching
    /// backward from <paramref name="startIndex"/> to the beginning of the string.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the backward search.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int LastIndexOf(char value, int startIndex, StringComparison comparisonType)
        => LastIndexOf(value, startIndex, startIndex + 1, comparisonType);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value, examining
    /// <paramref name="count"/> characters ending at (and including) <paramref name="startIndex"/>.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the backward search.</param>
    /// <param name="count">The number of characters to examine, counting backward from <paramref name="startIndex"/>.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int LastIndexOf(char value, int startIndex, int count, StringComparison comparisonType)
    {
        ReadOnlySpan<char> single = [value];
        return LastIndexOfCore(single, startIndex, count, comparisonType);
    }

    // --- string overloads ---

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    public int LastIndexOf(string? value)
        => LastIndexOf(value, InternalLength == 0 ? 0 : InternalLength - 1, InternalLength, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value, searching
    /// backward from <paramref name="startIndex"/> to the beginning of the string.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the backward search.</param>
    public int LastIndexOf(string? value, int startIndex)
        => LastIndexOf(value, startIndex, startIndex + 1, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value, examining
    /// <paramref name="count"/> characters ending at (and including) <paramref name="startIndex"/>.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the backward search.</param>
    /// <param name="count">The number of characters to examine, counting backward from <paramref name="startIndex"/>.</param>
    public int LastIndexOf(string? value, int startIndex, int count)
        => LastIndexOf(value, startIndex, count, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int LastIndexOf(string? value, StringComparison comparisonType)
        => LastIndexOf(value, InternalLength == 0 ? 0 : InternalLength - 1, InternalLength, comparisonType);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value, searching
    /// backward from <paramref name="startIndex"/> to the beginning of the string.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the backward search.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int LastIndexOf(string? value, int startIndex, StringComparison comparisonType)
        => LastIndexOf(value, startIndex, startIndex + 1, comparisonType);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value, examining
    /// <paramref name="count"/> characters ending at (and including) <paramref name="startIndex"/>.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the backward search.</param>
    /// <param name="count">The number of characters to examine, counting backward from <paramref name="startIndex"/>.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int LastIndexOf(string? value, int startIndex, int count, StringComparison comparisonType)
    {
        ArgumentNullException.ThrowIfNull(value);
        return LastIndexOfCore(value.AsSpan(), startIndex, count, comparisonType);
    }

    // --- MutableString overloads ---

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    public int LastIndexOf(MutableString? value)
        => LastIndexOf(value, InternalLength == 0 ? 0 : InternalLength - 1, InternalLength, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value, searching
    /// backward from <paramref name="startIndex"/> to the beginning of the string.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the backward search.</param>
    public int LastIndexOf(MutableString? value, int startIndex)
        => LastIndexOf(value, startIndex, startIndex + 1, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value, examining
    /// <paramref name="count"/> characters ending at (and including) <paramref name="startIndex"/>.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the backward search.</param>
    /// <param name="count">The number of characters to examine, counting backward from <paramref name="startIndex"/>.</param>
    public int LastIndexOf(MutableString? value, int startIndex, int count)
        => LastIndexOf(value, startIndex, count, StringComparison.Ordinal);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int LastIndexOf(MutableString? value, StringComparison comparisonType)
        => LastIndexOf(value, InternalLength == 0 ? 0 : InternalLength - 1, InternalLength, comparisonType);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value, searching
    /// backward from <paramref name="startIndex"/> to the beginning of the string.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the backward search.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int LastIndexOf(MutableString? value, int startIndex, StringComparison comparisonType)
        => LastIndexOf(value, startIndex, startIndex + 1, comparisonType);

    /// <summary>
    /// Returns the zero-based index of the last occurrence of the specified value, examining
    /// <paramref name="count"/> characters ending at (and including) <paramref name="startIndex"/>.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <param name="startIndex">The starting index of the backward search.</param>
    /// <param name="count">The number of characters to examine, counting backward from <paramref name="startIndex"/>.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    public int LastIndexOf(MutableString? value, int startIndex, int count, StringComparison comparisonType)
    {
        ArgumentNullException.ThrowIfNull(value);
        return LastIndexOfCore(value.AsSpan(), startIndex, count, comparisonType);
    }

    // --- shared core ---

    // Private implementation
    private int LastIndexOfCore(ReadOnlySpan<char> value, int startIndex, int count, StringComparison comparisonType)
    {
        if (InternalLength == 0)
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNotEqual(startIndex, 0);
            ArgumentOutOfRangeException.ThrowIfNotEqual(count, 0);
#else
        if (startIndex != 0)
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (count != 0)
            throw new ArgumentOutOfRangeException(nameof(count));
#endif
            return -1;
        }

        if (startIndex < 0 || startIndex >= InternalLength)
            throw new ArgumentOutOfRangeException(nameof(startIndex));

#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(count);
#else
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));
#endif

        if (count == 0)
            return -1;

        int windowStart = startIndex - count + 1;
#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(windowStart, nameof(count));
#else
    if (windowStart < 0)
        throw new ArgumentOutOfRangeException(nameof(count));
#endif

        int found = AsSpan().Slice(windowStart, count).LastIndexOf(value, comparisonType);
        return found < 0 ? -1 : found + windowStart;
    }

    #endregion

    #region LastIndexOfAny

    /// <summary>
    /// Returns the zero-based index of the last occurrence in this instance of any character in a specified array of Unicode
    /// characters.
    /// </summary>
    /// <param name="anyOf">An array of Unicode characters to find.</param>
    /// <returns>The zero-based index of the last occurrence of any character in this instance, or -1 if not found.</returns>
    public int LastIndexOfAny(char[] anyOf)
        => AsSpan().LastIndexOfAny(anyOf);

    /// <summary>
    /// Returns the zero-based index of the last occurrence in this instance of any character in a specified array of Unicode
    /// characters, starting at a specified character position.
    /// </summary>
    /// <param name="anyOf">An array of Unicode characters to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    /// <returns>The zero-based index of the last occurrence of any character in this instance, or -1 if not found.</returns>
    public int LastIndexOfAny(char[] anyOf, int startIndex)
        => AsSpan()[..(startIndex + 1)].LastIndexOfAny(anyOf);

    /// <summary>
    /// Returns the zero-based index of the last occurrence in this instance of any character in a specified array of Unicode
    /// characters, examining a specified number of characters.
    /// </summary>
    /// <param name="anyOf">An array of Unicode characters to find.</param>
    /// <param name="startIndex">The starting index of the search.</param>
    /// <param name="count">The number of characters to examine.</param>
    /// <returns>The zero-based index of the last occurrence of any character in this instance, or -1 if not found.</returns>
    public int LastIndexOfAny(char[] anyOf, int startIndex, int count)
        => AsSpan().Slice(startIndex - count + 1, count).LastIndexOfAny(anyOf);

    #endregion

    #region Contains

    /// <summary>
    /// Determines whether this <see cref="MutableString"/> contains the specified character.
    /// </summary>
    /// <param name="value">The character to find.</param>
    /// <returns><see langword="true" /> if it contains the specified character, <see langword="false" /> otherwise.</returns>
    public bool Contains(char value) => IndexOf(value) >= 0;

    /// <summary>
    /// Determines whether this <see cref="MutableString"/> contains the specified character.
    /// </summary>
    /// <param name="value">The character to find.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    /// <returns><see langword="true" /> if it contains the specified character, <see langword="false" /> otherwise.</returns>
    public bool Contains(char value, StringComparison comparisonType) => IndexOf(value, comparisonType) >= 0;

    /// <summary>
    /// Determines whether this <see cref="MutableString"/> contains the specified string.
    /// </summary>
    /// <param name="value">The string to find.</param>
    /// <returns><see langword="true" /> if it contains the specified string, <see langword="false" /> otherwise.</returns>
    public bool Contains(string? value) => IndexOf(value) >= 0;

    /// <summary>
    /// Determines whether this <see cref="MutableString"/> contains the specified string.
    /// </summary>
    /// <param name="value">The string to find.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    /// <returns><see langword="true" /> if it contains the specified string, <see langword="false" /> otherwise.</returns>
    public bool Contains(string? value, StringComparison comparisonType) => IndexOf(value, comparisonType) >= 0;

    /// <summary>
    /// Determines whether this <see cref="MutableString"/> contains the specified string.
    /// </summary>
    /// <param name="value">The string to find.</param>
    /// <returns><see langword="true" /> if it contains the specified string, <see langword="false" /> otherwise.</returns>
    public bool Contains(MutableString? value) => IndexOf(value) >= 0;

    /// <summary>
    /// Determines whether this <see cref="MutableString"/> contains the specified string.
    /// </summary>
    /// <param name="value">The string to find.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    /// <returns><see langword="true" /> if it contains the specified string, <see langword="false" /> otherwise.</returns>
    public bool Contains(MutableString? value, StringComparison comparisonType) => IndexOf(value, comparisonType) >= 0;

    #endregion

    #region StartsWith / EndsWith

    /// <summary>
    /// Determines whether this <see cref="MutableString"/> starts with the specified string.
    /// </summary>
    /// <param name="value">The string to compare to.</param>
    /// <returns><see langword="true" /> if <paramref name="value"/> matches the start, <see langword="false" /> otherwise.</returns>
    public bool StartsWith(char value) => InternalLength > 0 && Buffer[0] == value;

    /// <summary>
    /// Determines whether this <see cref="MutableString"/> starts with the specified string.
    /// </summary>
    /// <param name="value">The string to compare to.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    /// <returns><see langword="true" /> if <paramref name="value"/> matches the start, <see langword="false" /> otherwise.</returns>
    public bool StartsWith(string? value, StringComparison comparisonType = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(value);
        return AsSpan().StartsWith(value.AsSpan(), comparisonType);
    }

    /// <summary>
    /// Determines whether this <see cref="MutableString"/> starts with the specified string.
    /// </summary>
    /// <param name="value">The string to compare to.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    /// <returns><see langword="true" /> if <paramref name="value"/> matches the start, <see langword="false" /> otherwise.</returns>
    public bool StartsWith(MutableString? value, StringComparison comparisonType = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(value);
        return AsSpan().StartsWith(value.AsSpan(), comparisonType);
    }

    /// <summary>
    /// Determines whether this <see cref="MutableString"/> ends with the specified string.
    /// </summary>
    /// <param name="value">The string to compare to.</param>
    /// <returns><see langword="true" /> if <paramref name="value"/> matches the end, <see langword="false" /> otherwise.</returns>
    public bool EndsWith(char value) => InternalLength > 0 && Buffer[InternalLength - 1] == value;

    /// <summary>
    /// Determines whether this <see cref="MutableString"/> ends with the specified string.
    /// </summary>
    /// <param name="value">The string to compare to.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    /// <returns><see langword="true" /> if <paramref name="value"/> matches the end, <see langword="false" /> otherwise.</returns>
    public bool EndsWith(string? value, StringComparison comparisonType = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(value);
        return AsSpan().EndsWith(value.AsSpan(), comparisonType);
    }

    /// <summary>
    /// Determines whether this <see cref="MutableString"/> ends with the specified string.
    /// </summary>
    /// <param name="value">The string to compare to.</param>
    /// <param name="comparisonType">The type of comparison to perform.</param>
    /// <returns><see langword="true" /> if <paramref name="value"/> matches the end, <see langword="false" /> otherwise.</returns>
    public bool EndsWith(MutableString? value, StringComparison comparisonType = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(value);
        return AsSpan().EndsWith(value.AsSpan(), comparisonType);
    }

    #endregion

    #region Split

    /// <summary>
    /// Splits this <see cref="MutableString"/> into substrings based on the specified character
    /// delimiter and optional options.
    /// </summary>
    /// <param name="separator">The character delimiter.</param>
    /// <param name="options">Option flags.</param>
    public string[] Split(char separator, StringSplitOptions options = StringSplitOptions.None)
        => ToString().Split(separator, options);

    /// <summary>
    /// Splits this <see cref="MutableString"/> into substrings based on the specified string
    /// delimiter and optional options.
    /// </summary>
    /// <param name="separator">The string delimiter.</param>
    /// <param name="options">Option flags.</param>
    public string[] Split(string? separator, StringSplitOptions options = StringSplitOptions.None)
        => ToString().Split(separator, options);

    /// <summary>
    /// Splits this <see cref="MutableString"/> into substrings based on the specified characters.
    /// </summary>
    /// <param name="separator">The delimiter characters.</param>
    public string[] Split(params char[]? separator)
        => ToString().Split(separator);

    #endregion

}
