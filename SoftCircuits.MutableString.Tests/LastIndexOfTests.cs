/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests;

public class LastIndexOfTests
{
    // Reference string used throughout: "hello world hello"
    // Index:  0123456789012345 6
    //         h(0) e(1) l(2) l(3) o(4) space(5) w(6) o(7) r(8) l(9) d(10) space(11) h(12) e(13) l(14) l(15) o(16)
    // Length = 17
    private const string Sample = "hello world hello";

    // --- char overloads ---

    [Fact]
    public void LastIndexOf_Char_FindsLastOccurrence()
    {
        MutableString ms = new(Sample);
        Assert.Equal(12, ms.LastIndexOf('h'));
        Assert.Equal(16, ms.LastIndexOf('o'));
    }

    [Fact]
    public void LastIndexOf_Char_NotFound_ReturnsNegativeOne()
    {
        MutableString ms = new(Sample);
        Assert.Equal(-1, ms.LastIndexOf('z'));
    }

    [Fact]
    public void LastIndexOf_Char_WithStartIndex_OnlyExaminesUpToStartIndex()
    {
        MutableString ms = new(Sample);
        // Window is [0, 11] -- the second "hello" (starting at 12) is excluded entirely.
        Assert.Equal(0, ms.LastIndexOf('h', 11));
        // Window is [0, 16] -- the whole string.
        Assert.Equal(12, ms.LastIndexOf('h', 16));
    }

    [Fact]
    public void LastIndexOf_Char_WithStartIndexAndCount_LimitsSearchWindow()
    {
        MutableString ms = new(Sample);
        // Window [12, 16] = "hello" -> last 'o' at 16.
        Assert.Equal(16, ms.LastIndexOf('o', 16, 5));
        // Window [6, 10] = "world" -> only 'o' at 7.
        Assert.Equal(7, ms.LastIndexOf('o', 10, 5));
    }

    [Fact]
    public void LastIndexOf_Char_IsOrdinalByDefault_CaseSensitive()
    {
        MutableString ms = new("Hello");
        Assert.Equal(-1, ms.LastIndexOf('h'));
        Assert.Equal(0, ms.LastIndexOf('H'));
    }

    [Fact]
    public void LastIndexOf_Char_OrdinalIgnoreCase_FindsRegardlessOfCase()
    {
        MutableString ms = new("Hello");
        Assert.Equal(0, ms.LastIndexOf('h', StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void LastIndexOf_Char_CountZero_ReturnsNegativeOne()
    {
        MutableString ms = new("hello");
        Assert.Equal(-1, ms.LastIndexOf('h', 2, 0));
    }

    [Fact]
    public void LastIndexOf_Char_NegativeStartIndex_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.LastIndexOf('h', -1));
    }

    [Fact]
    public void LastIndexOf_Char_StartIndexEqualToLength_Throws()
    {
        // Deliberate deviation from real string.LastIndexOf (which permits startIndex == Length):
        // this implementation requires startIndex to be a valid in-bounds index.
        MutableString ms = new("hello");
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.LastIndexOf('h', 5));
    }

    [Fact]
    public void LastIndexOf_Char_StartIndexBeyondLength_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.LastIndexOf('h', 100));
    }

    [Fact]
    public void LastIndexOf_Char_NegativeCount_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.LastIndexOf('h', 2, -1));
    }

    [Fact]
    public void LastIndexOf_Char_CountExtendingBeforeStartOfString_Throws()
    {
        // startIndex=2, count=5 -> windowStart = 2-5+1 = -2, out of bounds.
        MutableString ms = new("hello");
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.LastIndexOf('h', 2, 5));
    }

    [Fact]
    public void LastIndexOf_Char_OnEmptyString_ReturnsNegativeOneWithoutThrowing()
    {
        MutableString ms = new();
        Assert.Equal(-1, ms.LastIndexOf('h'));
    }

    [Fact]
    public void LastIndexOf_Char_OnEmptyString_NonZeroStartIndex_Throws()
    {
        MutableString ms = new();
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.LastIndexOf('h', 1, 1));
    }

    // --- string overloads ---

    [Fact]
    public void LastIndexOf_String_FindsLastOccurrence()
    {
        MutableString ms = new(Sample);
        Assert.Equal(12, ms.LastIndexOf("hello"));
    }

    [Fact]
    public void LastIndexOf_String_NotFound_ReturnsNegativeOne()
    {
        MutableString ms = new(Sample);
        Assert.Equal(-1, ms.LastIndexOf("xyz"));
    }

    [Fact]
    public void LastIndexOf_String_WithStartIndex_ExcludesLaterOccurrence()
    {
        MutableString ms = new(Sample);
        // Window [0, 11] -- excludes the second "hello" entirely.
        Assert.Equal(0, ms.LastIndexOf("hello", 11));
        // Window [0, 16] -- the whole string.
        Assert.Equal(12, ms.LastIndexOf("hello", 16));
    }

    [Fact]
    public void LastIndexOf_String_WithStartIndexAndCount_LimitsSearchWindow()
    {
        MutableString ms = new(Sample);
        // Window [12, 16] = "hello" -> matches at 12.
        Assert.Equal(12, ms.LastIndexOf("hello", 16, 5));
        // Window [0, 4] = "hello" -> matches at 0.
        Assert.Equal(0, ms.LastIndexOf("hello", 4, 5));
    }

    [Fact]
    public void LastIndexOf_String_IsOrdinalByDefault_CaseSensitive()
    {
        MutableString ms = new(Sample);
        Assert.Equal(-1, ms.LastIndexOf("HELLO"));
    }

    [Fact]
    public void LastIndexOf_String_OrdinalIgnoreCase_FindsRegardlessOfCase()
    {
        MutableString ms = new(Sample);
        Assert.Equal(12, ms.LastIndexOf("HELLO", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void LastIndexOf_String_Null_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentNullException>(() => ms.LastIndexOf((string?)null));
    }

    [Fact]
    public void LastIndexOf_String_NegativeStartIndex_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.LastIndexOf("h", -1));
    }

    [Fact]
    public void LastIndexOf_String_StartIndexBeyondLength_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentOutOfRangeException>(() => ms.LastIndexOf("h", 100));
    }

    [Fact]
    public void LastIndexOf_String_LongerThanSearchWindow_ReturnsNegativeOne()
    {
        MutableString ms = new("hi");
        Assert.Equal(-1, ms.LastIndexOf("hello"));
    }

    // --- MutableString overloads ---

    [Fact]
    public void LastIndexOf_MutableString_FindsLastOccurrence()
    {
        MutableString ms = new(Sample);
        Assert.Equal(12, ms.LastIndexOf(new MutableString("hello")));
    }

    [Fact]
    public void LastIndexOf_MutableString_NotFound_ReturnsNegativeOne()
    {
        MutableString ms = new(Sample);
        Assert.Equal(-1, ms.LastIndexOf(new MutableString("xyz")));
    }

    [Fact]
    public void LastIndexOf_MutableString_WithStartIndex_ExcludesLaterOccurrence()
    {
        MutableString ms = new(Sample);
        Assert.Equal(0, ms.LastIndexOf(new MutableString("hello"), 11));
    }

    [Fact]
    public void LastIndexOf_MutableString_OrdinalIgnoreCase_FindsRegardlessOfCase()
    {
        MutableString ms = new(Sample);
        Assert.Equal(12, ms.LastIndexOf(new MutableString("HELLO"), StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void LastIndexOf_MutableString_Null_Throws()
    {
        MutableString ms = new("hello");
        Assert.Throws<ArgumentNullException>(() => ms.LastIndexOf((MutableString?)null));
    }

    [Fact]
    public void LastIndexOf_MutableString_OnlyUsesLogicalLength_NotBufferCapacity()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString needle = new("hello-extra");
#pragma warning restore IDE0017 // Simplify object initialization
        needle.Length = 5; // logically "hello"

        MutableString ms = new(Sample);
        Assert.Equal(12, ms.LastIndexOf(needle));
    }

    [Fact]
    public void LastIndexOf_MutableString_SearchTargetAlsoRespectsLogicalLength()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString ms = new("hello world extra");
#pragma warning restore IDE0017 // Simplify object initialization
        ms.Length = 11; // logically "hello world"; Buffer still holds "extra" beyond that

        Assert.Equal(-1, ms.LastIndexOf("extra"));
    }

    [Fact]
    public void LastIndexOf_MutableString_CanFindWithinItself()
    {
        MutableString ms = new("abcabc");
        Assert.Equal(3, ms.LastIndexOf(new MutableString("abc")));
    }
}