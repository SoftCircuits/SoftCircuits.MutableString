/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests;

public class RemoveTests
{
    [Fact]
    public void Remove_FromMiddle()
    {
        MutableString ms = new("hello world");
        ms.Remove(5, 1); // remove the space
        Assert.Equal("helloworld", ms.ToString());
    }

    [Fact]
    public void Remove_FromStart()
    {
        MutableString ms = new("hello world");
        ms.Remove(0, 6);
        Assert.Equal("world", ms.ToString());
    }

    [Fact]
    public void Remove_ToEnd()
    {
        MutableString ms = new("hello world");
        ms.Remove(5, 6);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Remove_EntireString()
    {
        MutableString ms = new("hello");
        ms.Remove(0, 5);
        Assert.Equal("", ms.ToString());
        Assert.Equal(0, ms.Length);
    }

    [Fact]
    public void Remove_CountExceedingAvailable_IsClamped()
    {
        MutableString ms = new("hello");
        ms.Remove(2, 1000);
        Assert.Equal("he", ms.ToString());
    }

    [Fact]
    public void Remove_ZeroCount_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Remove(2, 0);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Remove_NegativeCount_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Remove(2, -5);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Remove_NegativeIndex_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Remove(-1, 2);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Remove_IndexEqualToLength_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Remove(5, 1);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Remove_IndexBeyondLength_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Remove(1000, 1);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Remove_OnEmptyString_IsNoOp()
    {
        MutableString ms = new();
        ms.Remove(0, 1);
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void Remove_SingleCharacter_PreservesSurroundingText()
    {
        MutableString ms = new("0123456789");
        ms.Remove(4, 1);
        Assert.Equal("012356789", ms.ToString());
    }

    [Fact]
    public void Remove_UpdatesLengthCorrectly()
    {
        MutableString ms = new("hello world");
        ms.Remove(5, 6);
        Assert.Equal(5, ms.Length);
    }

    [Fact]
    public void Remove_FromStartToEnd()
    {
        MutableString ms = new("hello world");
        ms.Remove(0);
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void Remove_FromMiddleToEnd()
    {
        MutableString ms = new("hello world");
        ms.Remove(5);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Remove_FromEndToEnd()
    {
        MutableString ms = new("hello world");
        ms.Remove(ms.Length);
        Assert.Equal("hello world", ms.ToString());
    }

    // --- TrimStart ---

    [Fact]
    public void TrimStart_RemovesLeadingWhitespace()
    {
        MutableString ms = new("   hello");
        ms.TrimStart();
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void TrimStart_LeavesTrailingWhitespaceIntact()
    {
        MutableString ms = new("   hello   ");
        ms.TrimStart();
        Assert.Equal("hello   ", ms.ToString());
    }

    [Fact]
    public void TrimStart_NoLeadingWhitespace_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.TrimStart();
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void TrimStart_AllWhitespace_ProducesEmpty()
    {
        MutableString ms = new("   ");
        ms.TrimStart();
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void TrimStart_EmptyString_IsNoOp()
    {
        MutableString ms = new();
        ms.TrimStart();
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void TrimStart_HandlesTabsAndNewlines()
    {
        MutableString ms = new("\t\r\n hello");
        ms.TrimStart();
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void TrimStart_MutatesInPlace_UpdatesLength()
    {
        MutableString ms = new("   hello");
        ms.TrimStart();
        Assert.Equal(5, ms.Length);
    }

    // --- TrimEnd ---

    [Fact]
    public void TrimEnd_RemovesTrailingWhitespace()
    {
        MutableString ms = new("hello   ");
        ms.TrimEnd();
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void TrimEnd_LeavesLeadingWhitespaceIntact()
    {
        MutableString ms = new("   hello   ");
        ms.TrimEnd();
        Assert.Equal("   hello", ms.ToString());
    }

    [Fact]
    public void TrimEnd_NoTrailingWhitespace_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.TrimEnd();
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void TrimEnd_AllWhitespace_ProducesEmpty()
    {
        MutableString ms = new("   ");
        ms.TrimEnd();
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void TrimEnd_EmptyString_IsNoOp()
    {
        MutableString ms = new();
        ms.TrimEnd();
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void TrimEnd_MutatesInPlace_UpdatesLength()
    {
        MutableString ms = new("hello   ");
        ms.TrimEnd();
        Assert.Equal(5, ms.Length);
    }

    // --- Trim ---

    [Fact]
    public void Trim_RemovesLeadingAndTrailingWhitespace()
    {
        MutableString ms = new("   hello   ");
        ms.Trim();
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Trim_LeavesInternalWhitespaceIntact()
    {
        MutableString ms = new("   hello   world   ");
        ms.Trim();
        Assert.Equal("hello   world", ms.ToString());
    }

    [Fact]
    public void Trim_NoWhitespace_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Trim();
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Trim_AllWhitespace_ProducesEmpty()
    {
        MutableString ms = new("   \t\r\n  ");
        ms.Trim();
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void Trim_EmptyString_IsNoOp()
    {
        MutableString ms = new();
        ms.Trim();
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void Trim_LeadingOnlyWhitespace_TrimsCorrectly()
    {
        MutableString ms = new("   hello");
        ms.Trim();
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Trim_TrailingOnlyWhitespace_TrimsCorrectly()
    {
        MutableString ms = new("hello   ");
        ms.Trim();
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Trim_MutatesInPlace_UpdatesLength()
    {
        MutableString ms = new("   hello   ");
        ms.Trim();
        Assert.Equal(5, ms.Length);
    }

    [Fact]
    public void Trim_ChainsWithOtherMutatingMethods()
    {
        MutableString ms = new("   hello   ");
        ms.Trim();
        ms.Append("!");
        Assert.Equal("hello!", ms.ToString());
    }

    [Fact]
    public void Trim_SingleCharacterWhitespace_ProducesEmpty()
    {
        MutableString ms = new(" ");
        ms.Trim();
        Assert.Equal("", ms.ToString());
    }
}
