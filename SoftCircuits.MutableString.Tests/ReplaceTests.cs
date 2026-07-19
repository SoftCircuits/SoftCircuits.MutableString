namespace SoftCircuits.MutableString.Tests;

public class ReplaceTests
{
    [Fact]
    public void Replace_Grow_ReplacementLongerThanReplaced()
    {
        MutableString ms = new("hello world");
        ms.Replace(6, "there everyone", 5); // replaces "world" (5 chars)
        Assert.Equal("hello there everyone", ms.ToString());
    }

    [Fact]
    public void Replace_Shrink_ReplacementShorterThanReplaced()
    {
        MutableString ms = new("hello world");
        ms.Replace(0, "hi", 5); // replaces "hello" (5 chars)
        Assert.Equal("hi world", ms.ToString());
    }

    [Fact]
    public void Replace_EqualLength()
    {
        MutableString ms = new("hello world");
        ms.Replace(6, "WORLD", 5); // replaces "world" with same-length "WORLD"
        Assert.Equal("hello WORLD", ms.ToString());
    }

    [Fact]
    public void Replace_AtStart()
    {
        MutableString ms = new("aaabbb");
        ms.Replace(0, "XY", 3);
        Assert.Equal("XYbbb", ms.ToString());
    }

    [Fact]
    public void Replace_AtEnd()
    {
        MutableString ms = new("aaabbb");
        ms.Replace(3, "XY", 3);
        Assert.Equal("aaaXY", ms.ToString());
    }

    [Fact]
    public void Replace_EntireString_Grow()
    {
        MutableString ms = new("abc");
        ms.Replace(0, "hello world", 3);
        Assert.Equal("hello world", ms.ToString());
    }

    [Fact]
    public void Replace_EntireString_Shrink()
    {
        MutableString ms = new("hello world");
        ms.Replace(0, "hi", 11);
        Assert.Equal("hi", ms.ToString());
    }

    [Fact]
    public void Replace_ReplaceCountExceedingAvailable_IsClampedToRemainingLength()
    {
        MutableString ms = new("hello");
        ms.Replace(3, "XY", 10); // only "lo" (2 chars) remain from index 3
        Assert.Equal("helXY", ms.ToString());
    }

    [Fact]
    public void Replace_NegativeReplaceCount_ActsAsPureInsert()
    {
        MutableString ms = new("hello");
        ms.Replace(2, "XY", -5);
        Assert.Equal("heXYllo", ms.ToString());
    }

    [Fact]
    public void Replace_ZeroReplaceCount_ActsAsPureInsert()
    {
        MutableString ms = new("hello");
        ms.Replace(2, "XY", 0);
        Assert.Equal("heXYllo", ms.ToString());
    }

    [Fact]
    public void Replace_NegativeIndex_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Replace(-1, "XY", 2);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Replace_IndexBeyondLength_ActsAsAppend()
    {
        MutableString ms = new("hello");
        ms.Replace(1000, "!!", 3);
        Assert.Equal("hello!!", ms.ToString());
    }

    [Fact]
    public void Replace_EmptyReplacementString_DeletesReplacedRange()
    {
        MutableString ms = new("hello world");
        ms.Replace(5, "", 6); // deletes " world"
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Replace_NullReplacementString_DeletesReplacedRange()
    {
        MutableString ms = new("hello world");
        ms.Replace(5, (string?)null, 6);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Replace_CharArray_Works()
    {
        MutableString ms = new("hello world");
        ms.Replace(6, ['W', 'O', 'R', 'L', 'D'], 5);
        Assert.Equal("hello WORLD", ms.ToString());
    }

    [Fact]
    public void Replace_NullCharArray_DeletesReplacedRange()
    {
        MutableString ms = new("hello world");
        ms.Replace(5, (char[]?)null, 6);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Replace_Span_Works()
    {
        MutableString ms = new("hello world");
        ms.Replace(6, "WORLD".AsSpan(), 5);
        Assert.Equal("hello WORLD", ms.ToString());
    }

    [Fact]
    public void Replace_MutableString_Works()
    {
        MutableString ms = new("hello world");
        ms.Replace(6, new MutableString("WORLD"), 5);
        Assert.Equal("hello WORLD", ms.ToString());
    }

    [Fact]
    public void Replace_NullMutableString_DeletesReplacedRange()
    {
        MutableString ms = new("hello world");
        ms.Replace(5, (MutableString?)null, 6);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Replace_MutableString_OnlyUsesLogicalLength()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString source = new("WORLD-extra");
#pragma warning restore IDE0017 // Simplify object initialization
        source.Length = 5; // logically "WORLD"

        MutableString ms = new("hello world");
        ms.Replace(6, source, 5);

        Assert.Equal("hello WORLD", ms.ToString());
    }

    [Fact]
    public void Replace_LengthIsUpdatedCorrectly_Grow()
    {
        MutableString ms = new("abc");
        ms.Replace(1, "XYZW", 1);
        Assert.Equal(6, ms.Length);
    }

    [Fact]
    public void Replace_LengthIsUpdatedCorrectly_Shrink()
    {
        MutableString ms = new("abcdef");
        ms.Replace(1, "X", 4);
        Assert.Equal(3, ms.Length);
    }
}
