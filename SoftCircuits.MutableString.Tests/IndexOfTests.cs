/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests;

public class IndexOfTests
{
    // Reference string used throughout: "hello world hello"
    // Index:  0123456789012345678
    //         hello world hello
    //         h(0) e(1) l(2) l(3) o(4) space(5) w(6) o(7) r(8) l(9) d(10) space(11) h(12) e(13) l(14) l(15) o(16)
    private const string Sample = "hello world hello";

    // --- char overloads ---

    [Fact]
    public void IndexOf_Char_FindsFirstOccurrence()
    {
        MutableString ms = new(Sample);
        Assert.Equal(0, ms.IndexOf('h'));
        Assert.Equal(4, ms.IndexOf('o'));
    }

    [Fact]
    public void IndexOf_Char_NotFound_ReturnsNegativeOne()
    {
        MutableString ms = new(Sample);
        Assert.Equal(-1, ms.IndexOf('z'));
    }

    [Fact]
    public void IndexOf_Char_WithStartIndex_SkipsEarlierOccurrences()
    {
        MutableString ms = new(Sample);
        Assert.Equal(12, ms.IndexOf('h', 1));
        Assert.Equal(7, ms.IndexOf('o', 5));
    }

    [Fact]
    public void IndexOf_Char_StartIndexEqualToLength_ReturnsNegativeOne()
    {
        MutableString ms = new("hello");
        Assert.Equal(-1, ms.IndexOf('h', 5));
    }

    [Fact]
    public void IndexOf_Char_WithStartIndexAndCount_LimitsSearchRange()
    {
        MutableString ms = new("hello");
        Assert.Equal(-1, ms.IndexOf('l', 0, 2)); // only examines "he"
        Assert.Equal(2, ms.IndexOf('l', 0, 3));  // examines "hel"
    }

    [Fact]
    public void IndexOf_Char_IsOrdinalByDefault_CaseSensitive()
    {
        MutableString ms = new("Hello");
        Assert.Equal(-1, ms.IndexOf('h'));
        Assert.Equal(0, ms.IndexOf('H'));
    }

    [Fact]
    public void IndexOf_Char_OrdinalIgnoreCase_FindsRegardlessOfCase()
    {
        MutableString ms = new("Hello");
        Assert.Equal(0, ms.IndexOf('h', StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void IndexOf_Char_NegativeStartIndex_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.IndexOf('h', -1));
    }

    [Fact]
    public void IndexOf_Char_StartIndexBeyondLength_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.IndexOf('h', 100));
    }

    [Fact]
    public void IndexOf_Char_NegativeCount_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.IndexOf('h', 0, -1));
    }

    [Fact]
    public void IndexOf_Char_CountExceedingRemainingLength_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.IndexOf('h', 0, 100));
    }

    // --- string overloads ---

    [Fact]
    public void IndexOf_String_FindsFirstOccurrence()
    {
        MutableString ms = new(Sample);
        Assert.Equal(6, ms.IndexOf("world"));
        Assert.Equal(0, ms.IndexOf("hello"));
    }

    [Fact]
    public void IndexOf_String_NotFound_ReturnsNegativeOne()
    {
        MutableString ms = new(Sample);
        Assert.Equal(-1, ms.IndexOf("xyz"));
    }

    [Fact]
    public void IndexOf_String_WithStartIndex_SkipsEarlierOccurrences()
    {
        MutableString ms = new(Sample);
        Assert.Equal(12, ms.IndexOf("hello", 1));
    }

    [Fact]
    public void IndexOf_String_WithStartIndex_NoFurtherOccurrence_ReturnsNegativeOne()
    {
        MutableString ms = new(Sample);
        Assert.Equal(-1, ms.IndexOf("world", 7));
    }

    [Fact]
    public void IndexOf_String_WithStartIndexAndCount_LimitsSearchRange()
    {
        MutableString ms = new(Sample);
        Assert.Equal(-1, ms.IndexOf("world", 0, 5)); // "hello" only, "world" starts at 6
        Assert.Equal(6, ms.IndexOf("world", 0, 11));
    }

    [Fact]
    public void IndexOf_String_EmptyValue_ReturnsStartIndex()
    {
        MutableString ms = new("hello");
        Assert.Equal(0, ms.IndexOf(""));
        Assert.Equal(2, ms.IndexOf("", 2));
    }

    [Fact]
    public void IndexOf_String_IsOrdinalByDefault_CaseSensitive()
    {
        MutableString ms = new(Sample);
        Assert.Equal(-1, ms.IndexOf("WORLD"));
    }

    [Fact]
    public void IndexOf_String_OrdinalIgnoreCase_FindsRegardlessOfCase()
    {
        MutableString ms = new(Sample);
        Assert.Equal(6, ms.IndexOf("WORLD", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void IndexOf_String_Null_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentNullException>(() => ms.IndexOf((string?)null));
    }

    [Fact]
    public void IndexOf_String_NegativeStartIndex_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.IndexOf("h", -1));
    }

    [Fact]
    public void IndexOf_String_StartIndexBeyondLength_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.IndexOf("h", 100));
    }

    [Fact]
    public void IndexOf_String_CountExceedingRemainingLength_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.IndexOf("h", 0, 100));
    }

    [Fact]
    public void IndexOf_String_LongerThanSearchRange_ReturnsNegativeOne()
    {
        MutableString ms = new("hi");
        Assert.Equal(-1, ms.IndexOf("hello"));
    }

    // --- MutableString overloads ---

    [Fact]
    public void IndexOf_MutableString_FindsFirstOccurrence()
    {
        MutableString ms = new(Sample);
        Assert.Equal(6, ms.IndexOf(new MutableString("world")));
    }

    [Fact]
    public void IndexOf_MutableString_NotFound_ReturnsNegativeOne()
    {
        MutableString ms = new(Sample);
        Assert.Equal(-1, ms.IndexOf(new MutableString("xyz")));
    }

    [Fact]
    public void IndexOf_MutableString_WithStartIndex_SkipsEarlierOccurrences()
    {
        MutableString ms = new(Sample);
        Assert.Equal(12, ms.IndexOf(new MutableString("hello"), 1));
    }

    [Fact]
    public void IndexOf_MutableString_OrdinalIgnoreCase_FindsRegardlessOfCase()
    {
        MutableString ms = new(Sample);
        Assert.Equal(6, ms.IndexOf(new MutableString("WORLD"), StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void IndexOf_MutableString_Null_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentNullException>(() => ms.IndexOf((MutableString?)null));
    }

    [Fact]
    public void IndexOf_MutableString_OnlyUsesLogicalLength_NotBufferCapacity()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString needle = new("world-extra");
#pragma warning restore IDE0017 // Simplify object initialization
        needle.Length = 5; // logically "world"

        MutableString ms = new(Sample);
        Assert.Equal(6, ms.IndexOf(needle));
    }

    [Fact]
    public void IndexOf_MutableString_SearchTargetAlsoRespectsLogicalLength()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString ms = new("hello world extra");
#pragma warning restore IDE0017 // Simplify object initialization
        ms.Length = 11; // logically "hello world"; Buffer still holds "extra" beyond that

        Assert.Equal(-1, ms.IndexOf("extra"));
    }

    [Fact]
    public void IndexOf_MutableString_CanFindWithinItself()
    {
        MutableString ms = new("abcabc");
        Assert.Equal(0, ms.IndexOf(new MutableString("abc")));
        Assert.Equal(3, ms.IndexOf(new MutableString("abc"), 1));
    }

    // --- IndexOfAny tests ---

    [Fact]
    public void IndexOfAny_NotFound_ReturnsNegativeOne()
    {
        MutableString ms = new("abcdef");
        Assert.Equal(-1, ms.IndexOfAny(['x', 'y', 'z']));
    }

    [Fact]
    public void IndexOfAny_FindFirstCharacter()
    {
        MutableString ms = new("abcdef");
        Assert.Equal(0, ms.IndexOfAny(['a', 'x', 'y', 'z']));
    }

    [Fact]
    public void IndexOfAny_FindMiddleCharacter()
    {
        MutableString ms = new("abcdef");
        Assert.Equal(2, ms.IndexOfAny(['c','x','y','z']));
    }

    [Fact]
    public void IndexOfAny_FindLastCharacter()
    {
        MutableString ms = new("abcdef");
        Assert.Equal(5, ms.IndexOfAny(['f', 'x', 'y', 'z']));
    }

    [Fact]
    public void IndexOfAny_UseSpecifiedCount()
    {
        MutableString ms = new("aaaaaz");
        Assert.Equal(-1, ms.IndexOfAny(['x', 'y', 'z'], 0, 5));
    }

    [Fact]
    public void IndexOfAny_UsesLogicalLength()
    {
        MutableString ms = new("aaaaaz");
        Assert.Equal(5, ms.IndexOfAny(['x', 'y', 'z']));
        ms.Length = 5;
        Assert.Equal(-1, ms.IndexOfAny(['x', 'y', 'z']));
    }
}
