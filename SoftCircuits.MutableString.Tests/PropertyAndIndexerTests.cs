/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests;

public class PropertyAndIndexerTests
{
    [Fact]
    public void Length_Get_ReturnsCharacterCount()
    {
        MutableString ms = new("hello");
        Assert.Equal(5, ms.Length);
    }

    [Fact]
    public void Length_SetLarger_OnFreshInstance_ZeroFillsNewCharacters()
    {
        // A freshly constructed MutableString has a zero-filled internal buffer with
        // spare capacity (Resize allocates at least 32 chars), so growing Length here
        // does not trigger reallocation and the added characters are guaranteed '\0'.
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString ms = new();
#pragma warning restore IDE0017 // Simplify object initialization
        ms.Length = 5;
        Assert.Equal(5, ms.Length);
        Assert.Equal(new string('\0', 5), ms.ToString());
    }

    [Fact]
    public void Length_SetSmaller_TruncatesContent()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString ms = new("hello world");
#pragma warning restore IDE0017 // Simplify object initialization
        ms.Length = 5;
        Assert.Equal(5, ms.Length);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Length_SetToZero_ProducesEmptyString()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString ms = new("hello");
#pragma warning restore IDE0017 // Simplify object initialization
        ms.Length = 0;
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void Length_SetNegative_ClampsToZero()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString ms = new("hello");
#pragma warning restore IDE0017 // Simplify object initialization
        ms.Length = -5;
        Assert.Equal(0, ms.Length);
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void IsEmpty_TrueForNewInstance()
    {
        MutableString ms = new();
        Assert.True(ms.IsEmpty);
    }

    [Fact]
    public void IsEmpty_FalseWhenContainsCharacters()
    {
        MutableString ms = new("a");
        Assert.False(ms.IsEmpty);
    }

    [Fact]
    public void IsEmpty_TrueAfterClearingContent()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString ms = new("hello");
#pragma warning restore IDE0017 // Simplify object initialization
        ms.Length = 0;
        Assert.True(ms.IsEmpty);
    }

    [Fact]
    public void IsWhiteSpace_TrueForEmptyString()
    {
        MutableString ms = new();
        Assert.True(ms.IsWhiteSpace);
    }

    [Theory]
    [InlineData("   ")]
    [InlineData("\t\t")]
    [InlineData("\r\n")]
    [InlineData(" \t\r\n ")]
    public void IsWhiteSpace_TrueForWhitespaceOnly(string s)
    {
        MutableString ms = new(s);
        Assert.True(ms.IsWhiteSpace);
    }

    [Theory]
    [InlineData("hello")]
    [InlineData(" hello ")]
    [InlineData("a")]
    public void IsWhiteSpace_FalseWhenNonWhitespacePresent(string s)
    {
        MutableString ms = new(s);
        Assert.False(ms.IsWhiteSpace);
    }

    [Fact]
    public void IntIndexer_Get_ReturnsCorrectCharacter()
    {
        MutableString ms = new("hello");
        Assert.Equal('h', ms[0]);
        Assert.Equal('e', ms[1]);
        Assert.Equal('o', ms[4]);
    }

    [Fact]
    public void IntIndexer_Set_ChangesCharacter()
    {
        MutableString ms = new("hello");
        ms[0] = 'j';
        Assert.Equal("jello", ms.ToString());
    }

    [Fact]
    public void IntIndexer_Get_IndexAtOrBeyondLength_Throws()
    {
        MutableString ms = new(32); // capacity well beyond Length
        ms.Append("abc");
        Assert.Throws<IndexOutOfRangeException>(() => _ = ms[10]);
    }

    [Fact]
    public void IntIndexer_Get_TrulyOutOfBufferBounds_Throws()
    {
        MutableString ms = new("abc");
        Assert.Throws<IndexOutOfRangeException>(() => _ = ms[1000]);
    }

    [Fact]
    public void IntIndexer_Get_NegativeIndex_Throws()
    {
        MutableString ms = new("abc");
        Assert.Throws<IndexOutOfRangeException>(() => _ = ms[-1]);
    }

    [Fact]
    public void IndexIndexer_Get_FromStart()
    {
        MutableString ms = new("hello");
        Assert.Equal('h', ms[Index.FromStart(0)]);
    }

    [Fact]
    public void IndexIndexer_Get_FromEnd()
    {
        MutableString ms = new("hello");
        Assert.Equal('o', ms[^1]);
        Assert.Equal('l', ms[^2]);
    }

    [Fact]
    public void IndexIndexer_Set_FromEnd()
    {
        MutableString ms = new("hello");
        ms[^1] = 'y';
        Assert.Equal("helly", ms.ToString());
    }

    [Fact]
    public void RangeIndexer_ReturnsSubstring()
    {
        MutableString ms = new("hello world");
        Assert.Equal("hello", ms[0..5]);
        Assert.Equal("world", ms[6..11]);
        Assert.Equal("world", ms[6..]);
        Assert.Equal("hello", ms[..5]);
    }

    [Fact]
    public void RangeIndexer_FromEndRange()
    {
        MutableString ms = new("hello world");
        Assert.Equal("world", ms[^5..]);
    }

    [Fact]
    public void RangeIndexer_EmptyRange()
    {
        MutableString ms = new("hello");
        Assert.Equal("", ms[2..2]);
    }

    [Fact]
    public void RangeIndexer_FullRange()
    {
        MutableString ms = new("hello");
        Assert.Equal("hello", ms[..]);
    }
}
