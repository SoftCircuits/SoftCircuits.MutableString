/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests;

public class ContainsTests
{
    private const string Sample = "hello world";

    // --- char ---

    [Fact]
    public void Contains_Char_True()
    {
        MutableString ms = new(Sample);
        Assert.True(ms.Contains('w'));
    }

    [Fact]
    public void Contains_Char_False()
    {
        MutableString ms = new(Sample);
        Assert.False(ms.Contains('z'));
    }

    [Fact]
    public void Contains_Char_IsOrdinalByDefault_CaseSensitive()
    {
        MutableString ms = new("Hello");
        Assert.False(ms.Contains('h'));
        Assert.True(ms.Contains('h', StringComparison.OrdinalIgnoreCase));
    }

    // --- string ---

    [Fact]
    public void Contains_String_True()
    {
        MutableString ms = new(Sample);
        Assert.True(ms.Contains("world"));
    }

    [Fact]
    public void Contains_String_False()
    {
        MutableString ms = new(Sample);
        Assert.False(ms.Contains("xyz"));
    }

    [Fact]
    public void Contains_String_EmptyValue_IsTrue()
    {
        // Matches IndexOf("") returning 0 (a match at the start) for any string.
        MutableString ms = new(Sample);
        Assert.True(ms.Contains(""));
    }

    [Fact]
    public void Contains_String_IsOrdinalByDefault_CaseSensitive()
    {
        MutableString ms = new(Sample);
        Assert.False(ms.Contains("WORLD"));
        Assert.True(ms.Contains("WORLD", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Contains_String_Null_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentNullException>(() => ms.Contains((string?)null));
    }

    [Fact]
    public void Contains_String_OnEmptyMutableString_OnlyEmptyValueMatches()
    {
        MutableString ms = new();
        Assert.True(ms.Contains(""));
        Assert.False(ms.Contains("a"));
    }

    // --- MutableString ---

    [Fact]
    public void Contains_MutableString_True()
    {
        MutableString ms = new(Sample);
        Assert.True(ms.Contains(new MutableString("world")));
    }

    [Fact]
    public void Contains_MutableString_False()
    {
        MutableString ms = new(Sample);
        Assert.False(ms.Contains(new MutableString("xyz")));
    }

    [Fact]
    public void Contains_MutableString_OrdinalIgnoreCase()
    {
        MutableString ms = new(Sample);
        Assert.True(ms.Contains(new MutableString("WORLD"), StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Contains_MutableString_Null_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentNullException>(() => ms.Contains((MutableString?)null));
    }

    [Fact]
    public void Contains_MutableString_OnlyUsesLogicalLength()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString needle = new("world-extra");
#pragma warning restore IDE0017 // Simplify object initialization
        needle.Length = 5; // logically "world"

        MutableString ms = new(Sample);
        Assert.True(ms.Contains(needle));
    }

    // --- StartsWith(char) ---

    [Fact]
    public void StartsWith_Char_True()
    {
        MutableString ms = new("hello");
        Assert.True(ms.StartsWith('h'));
    }

    [Fact]
    public void StartsWith_Char_False()
    {
        MutableString ms = new("hello");
        Assert.False(ms.StartsWith('e'));
    }

    [Fact]
    public void StartsWith_Char_IsCaseSensitive()
    {
        MutableString ms = new("Hello");
        Assert.False(ms.StartsWith('h'));
        Assert.True(ms.StartsWith('H'));
    }

    [Fact]
    public void StartsWith_Char_OnEmptyString_ReturnsFalse()
    {
        MutableString ms = new();
        Assert.False(ms.StartsWith('h'));
    }

    // --- StartsWith(string) ---

    [Fact]
    public void StartsWith_String_True()
    {
        MutableString ms = new("hello world");
        Assert.True(ms.StartsWith("hello"));
    }

    [Fact]
    public void StartsWith_String_False()
    {
        MutableString ms = new("hello world");
        Assert.False(ms.StartsWith("world"));
    }

    [Fact]
    public void StartsWith_String_LongerThanSource_ReturnsFalse()
    {
        MutableString ms = new("hi");
        Assert.False(ms.StartsWith("hello"));
    }

    [Fact]
    public void StartsWith_String_EmptyValue_ReturnsTrue()
    {
        MutableString ms = new("hello");
        Assert.True(ms.StartsWith(""));
    }

    [Fact]
    public void StartsWith_String_ExactMatch_ReturnsTrue()
    {
        MutableString ms = new("hello");
        Assert.True(ms.StartsWith("hello"));
    }

    [Fact]
    public void StartsWith_String_IsOrdinalByDefault_CaseSensitive()
    {
        MutableString ms = new("Hello world");
        Assert.False(ms.StartsWith("hello"));
    }

    [Fact]
    public void StartsWith_String_OrdinalIgnoreCase_MatchesRegardlessOfCase()
    {
        MutableString ms = new("Hello world");
        Assert.True(ms.StartsWith("hello", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void StartsWith_String_Null_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentNullException>(() => ms.StartsWith((string?)null));
    }

    [Fact]
    public void StartsWith_String_OnEmptyMutableString_OnlyEmptyValueMatches()
    {
        MutableString ms = new();
        Assert.True(ms.StartsWith(""));
        Assert.False(ms.StartsWith("a"));
    }

    // --- StartsWith(MutableString) ---

    [Fact]
    public void StartsWith_MutableString_True()
    {
        MutableString ms = new("hello world");
        Assert.True(ms.StartsWith(new MutableString("hello")));
    }

    [Fact]
    public void StartsWith_MutableString_False()
    {
        MutableString ms = new("hello world");
        Assert.False(ms.StartsWith(new MutableString("world")));
    }

    [Fact]
    public void StartsWith_MutableString_OrdinalIgnoreCase()
    {
        MutableString ms = new("Hello world");
        Assert.True(ms.StartsWith(new MutableString("hello"), StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void StartsWith_MutableString_Null_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentNullException>(() => ms.StartsWith((MutableString?)null));
    }

    [Fact]
    public void StartsWith_MutableString_OnlyUsesLogicalLength_NotBufferCapacity()
    {
        // Regression check: must respect Count, not leftover Buffer capacity, on both sides.
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString value = new("hello-extra");
#pragma warning restore IDE0017 // Simplify object initialization
        value.Length = 5; // logically "hello"

#pragma warning disable IDE0017 // Simplify object initialization
        MutableString ms = new("hello world extra");
#pragma warning restore IDE0017 // Simplify object initialization
        ms.Length = 11; // logically "hello world"

        Assert.True(ms.StartsWith(value));
    }

    // --- EndsWith(char) ---

    [Fact]
    public void EndsWith_Char_True()
    {
        MutableString ms = new("hello");
        Assert.True(ms.EndsWith('o'));
    }

    [Fact]
    public void EndsWith_Char_False()
    {
        MutableString ms = new("hello");
        Assert.False(ms.EndsWith('l'));
    }

    [Fact]
    public void EndsWith_Char_IsCaseSensitive()
    {
        MutableString ms = new("hellO");
        Assert.False(ms.EndsWith('o'));
        Assert.True(ms.EndsWith('O'));
    }

    [Fact]
    public void EndsWith_Char_OnEmptyString_ReturnsFalse()
    {
        MutableString ms = new();
        Assert.False(ms.EndsWith('o'));
    }

    [Fact]
    public void EndsWith_Char_SingleCharacterString()
    {
        MutableString ms = new("x");
        Assert.True(ms.EndsWith('x'));
        Assert.False(ms.EndsWith('y'));
    }

    // --- EndsWith(string) ---

    [Fact]
    public void EndsWith_String_True()
    {
        MutableString ms = new("hello world");
        Assert.True(ms.EndsWith("world"));
    }

    [Fact]
    public void EndsWith_String_False()
    {
        MutableString ms = new("hello world");
        Assert.False(ms.EndsWith("hello"));
    }

    [Fact]
    public void EndsWith_String_LongerThanSource_ReturnsFalse()
    {
        MutableString ms = new("hi");
        Assert.False(ms.EndsWith("hello"));
    }

    [Fact]
    public void EndsWith_String_EmptyValue_ReturnsTrue()
    {
        MutableString ms = new("hello");
        Assert.True(ms.EndsWith(""));
    }

    [Fact]
    public void EndsWith_String_ExactMatch_ReturnsTrue()
    {
        MutableString ms = new("hello");
        Assert.True(ms.EndsWith("hello"));
    }

    [Fact]
    public void EndsWith_String_IsOrdinalByDefault_CaseSensitive()
    {
        MutableString ms = new("hello World");
        Assert.False(ms.EndsWith("world"));
    }

    [Fact]
    public void EndsWith_String_OrdinalIgnoreCase_MatchesRegardlessOfCase()
    {
        MutableString ms = new("hello World");
        Assert.True(ms.EndsWith("world", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void EndsWith_String_Null_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentNullException>(() => ms.EndsWith((string?)null));
    }

    [Fact]
    public void EndsWith_String_OnEmptyMutableString_OnlyEmptyValueMatches()
    {
        MutableString ms = new();
        Assert.True(ms.EndsWith(""));
        Assert.False(ms.EndsWith("a"));
    }

    // --- EndsWith(MutableString) ---

    [Fact]
    public void EndsWith_MutableString_True()
    {
        MutableString ms = new("hello world");
        Assert.True(ms.EndsWith(new MutableString("world")));
    }

    [Fact]
    public void EndsWith_MutableString_False()
    {
        MutableString ms = new("hello world");
        Assert.False(ms.EndsWith(new MutableString("hello")));
    }

    [Fact]
    public void EndsWith_MutableString_OrdinalIgnoreCase()
    {
        MutableString ms = new("hello World");
        Assert.True(ms.EndsWith(new MutableString("world"), StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void EndsWith_MutableString_Null_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentNullException>(() => ms.EndsWith((MutableString?)null));
    }

    [Fact]
    public void EndsWith_MutableString_OnlyUsesLogicalLength_NotBufferCapacity()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString value = new("world-extra");
#pragma warning restore IDE0017 // Simplify object initialization
        value.Length = 5; // logically "world"

#pragma warning disable IDE0017 // Simplify object initialization
        MutableString ms = new("hello world extra");
#pragma warning restore IDE0017 // Simplify object initialization
        ms.Length = 11; // logically "hello world"

        Assert.True(ms.EndsWith(value));
    }

    // --- combined sanity checks ---

    [Fact]
    public void StartsWithAndEndsWith_SameShortString_BothTrue()
    {
        MutableString ms = new("ab");
        Assert.True(ms.StartsWith("ab"));
        Assert.True(ms.EndsWith("ab"));
    }

    [Fact]
    public void StartsWithAndEndsWith_OverlappingMatch_BothTrue()
    {
        // "aba" starts and ends with "a" even though it's the same character position count.
        MutableString ms = new("aba");
        Assert.True(ms.StartsWith('a'));
        Assert.True(ms.EndsWith('a'));
    }

    // --- Split(char, StringSplitOptions) ---

    [Fact]
    public void Split_Char_SplitsOnDelimiter()
    {
        MutableString ms = new("a,b,c");
        string[] result = ms.Split(',');
        Assert.Equal(["a", "b", "c"], result);
    }

    [Fact]
    public void Split_Char_NoDelimiterPresent_ReturnsWholeStringAsSingleElement()
    {
        MutableString ms = new("hello");
        string[] result = ms.Split(',');
        Assert.Equal(["hello"], result);
    }

    [Fact]
    public void Split_Char_AdjacentDelimiters_ProduceEmptyEntries()
    {
        MutableString ms = new("a,,b");
        string[] result = ms.Split(',');
        Assert.Equal(["a", "", "b"], result);
    }

    [Fact]
    public void Split_Char_RemoveEmptyEntries_OmitsEmptyStrings()
    {
        MutableString ms = new("a,,b");
        string[] result = ms.Split(',', StringSplitOptions.RemoveEmptyEntries);
        Assert.Equal(["a", "b"], result);
    }

    [Fact]
    public void Split_Char_EmptyMutableString_ReturnsSingleEmptyElement()
    {
        MutableString ms = new();
        string[] result = ms.Split(',');
        Assert.Equal([""], result);
    }

    [Fact]
    public void Split_Char_OnlyUsesLogicalLength_NotBufferCapacity()
    {
        // Regression check: split must operate on Count, not leftover Buffer capacity.
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString ms = new("a,b,c,extra");
#pragma warning restore IDE0017 // Simplify object initialization
        ms.Length = 5; // logically "a,b,c"

        string[] result = ms.Split(',');
        Assert.Equal(["a", "b", "c"], result);
    }

    // --- Split(string?, StringSplitOptions) ---

    [Fact]
    public void Split_String_SplitsOnMultiCharacterDelimiter()
    {
        MutableString ms = new("aXXbXXc");
        string[] result = ms.Split("XX");
        Assert.Equal(["a", "b", "c"], result);
    }

    [Fact]
    public void Split_String_NoDelimiterPresent_ReturnsWholeStringAsSingleElement()
    {
        MutableString ms = new("hello");
        string[] result = ms.Split("XX");
        Assert.Equal(["hello"], result);
    }

    [Fact]
    public void Split_String_RemoveEmptyEntries_OmitsEmptyStrings()
    {
        MutableString ms = new("aXXXXb");
        string[] result = ms.Split("XX", StringSplitOptions.RemoveEmptyEntries);
        Assert.Equal(["a", "b"], result);
    }

    [Fact]
    public void Split_String_OnlyUsesLogicalLength_NotBufferCapacity()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString ms = new("aXXbXXc-extra");
#pragma warning restore IDE0017 // Simplify object initialization
        ms.Length = 7; // logically "aXXbXXc"

        string[] result = ms.Split("XX");
        Assert.Equal(["a", "b", "c"], result);
    }

    // --- Split(params char[]?) ---

    [Fact]
    public void Split_CharArray_SplitsOnAnyProvidedDelimiter()
    {
        MutableString ms = new("a,b;c");
        string[] result = ms.Split(',', ';');
        Assert.Equal(["a", "b", "c"], result);
    }

    [Fact]
    public void Split_CharArray_SingleDelimiter()
    {
        MutableString ms = new("a b c");
        string[] result = ms.Split(' ');
        Assert.Equal(["a", "b", "c"], result);
    }

    [Fact]
    public void Split_CharArray_NoDelimiterPresent_ReturnsWholeStringAsSingleElement()
    {
        MutableString ms = new("hello");
        string[] result = ms.Split(',', ';');
        Assert.Equal(["hello"], result);
    }

    [Fact]
    public void Split_CharArray_OnlyUsesLogicalLength_NotBufferCapacity()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString ms = new("a,b;c-extra");
#pragma warning restore IDE0017 // Simplify object initialization
        ms.Length = 5; // logically "a,b;c"

        string[] result = ms.Split(',', ';');
        Assert.Equal(["a", "b", "c"], result);
    }
}
