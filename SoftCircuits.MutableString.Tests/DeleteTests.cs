/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests;

public class DeleteTests
{
    [Fact]
    public void Delete_FromMiddle()
    {
        MutableString ms = new("hello world");
        ms.Delete(5, 1); // remove the space
        Assert.Equal("helloworld", ms.ToString());
    }

    [Fact]
    public void Delete_FromStart()
    {
        MutableString ms = new("hello world");
        ms.Delete(0, 6);
        Assert.Equal("world", ms.ToString());
    }

    [Fact]
    public void Delete_ToEnd()
    {
        MutableString ms = new("hello world");
        ms.Delete(5, 6);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Delete_EntireString()
    {
        MutableString ms = new("hello");
        ms.Delete(0, 5);
        Assert.Equal("", ms.ToString());
        Assert.Equal(0, ms.Length);
    }

    [Fact]
    public void Delete_CountExceedingAvailable_IsClamped()
    {
        MutableString ms = new("hello");
        ms.Delete(2, 1000);
        Assert.Equal("he", ms.ToString());
    }

    [Fact]
    public void Delete_ZeroCount_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Delete(2, 0);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Delete_NegativeCount_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Delete(2, -5);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Delete_NegativeIndex_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Delete(-1, 2);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Delete_IndexEqualToLength_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Delete(5, 1);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Delete_IndexBeyondLength_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Delete(1000, 1);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Delete_OnEmptyString_IsNoOp()
    {
        MutableString ms = new();
        ms.Delete(0, 1);
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void Delete_SingleCharacter_PreservesSurroundingText()
    {
        MutableString ms = new("0123456789");
        ms.Delete(4, 1);
        Assert.Equal("012356789", ms.ToString());
    }

    [Fact]
    public void Delete_UpdatesLengthCorrectly()
    {
        MutableString ms = new("hello world");
        ms.Delete(5, 6);
        Assert.Equal(5, ms.Length);
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

    [Fact]
    public void TrimStart_ReturnsSameInstance_ForChaining()
    {
        MutableString ms = new("   hello");
        MutableString result = ms.TrimStart();
        Assert.Same(ms, result);
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

    [Fact]
    public void TrimEnd_ReturnsSameInstance_ForChaining()
    {
        MutableString ms = new("hello   ");
        MutableString result = ms.TrimEnd();
        Assert.Same(ms, result);
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
    public void Trim_ReturnsSameInstance_ForChaining()
    {
        MutableString ms = new("   hello   ");
        MutableString result = ms.Trim();
        Assert.Same(ms, result);
    }

    [Fact]
    public void Trim_ChainsWithOtherMutatingMethods()
    {
        MutableString ms = new("   hello   ");
        ms.Trim().Append("!");
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
