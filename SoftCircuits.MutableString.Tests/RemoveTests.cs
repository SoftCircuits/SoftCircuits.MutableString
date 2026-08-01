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
}
