/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests;

public class TrimTests
{
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
