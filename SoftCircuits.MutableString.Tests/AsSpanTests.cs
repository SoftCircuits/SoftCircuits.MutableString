/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests;

public class AsSpanTests
{

    [Fact]
    public void AsSpan_EmptyString()
    {
        MutableString ms = new();
        ReadOnlySpan<char> span = ms.AsSpan();
        Assert.Equal(0, span.Length);
    }

    [Fact]
    public void AsSpan_EntireString()
    {
        MutableString ms = new("hello");
        ReadOnlySpan<char> span = ms.AsSpan();
        Assert.Equal(5, span.Length);
        Assert.Equal("hello", span.ToString());
    }

    [Fact]
    public void AsSpan_SetStart()
    {
        MutableString ms = new("hello");
        ReadOnlySpan<char> span = ms.AsSpan(2);
        Assert.Equal(3, span.Length);
        Assert.Equal("llo", span.ToString());
    }

    [Fact]
    public void AsSpan_SetStartEmpty()
    {
        MutableString ms = new("hello");
        ReadOnlySpan<char> span = ms.AsSpan(5);
        Assert.Equal(0, span.Length);
    }

    [Fact]
    public void AsSpan_SetStartAndLength()
    {
        MutableString ms = new("hello");
        ReadOnlySpan<char> span = ms.AsSpan(1, 2);
        Assert.Equal(2, span.Length);
        Assert.Equal("el", span.ToString());
    }

    [Fact]
    public void AsSpan_SetStartAndLengthToEnd()
    {
        MutableString ms = new("hello");
        ReadOnlySpan<char> span = ms.AsSpan(1, 4);
        Assert.Equal(4, span.Length);
        Assert.Equal("ello", span.ToString());
    }

    [Fact]
    public void AsSpan_SetStartAndLengthEmpty()
    {
        MutableString ms = new("hello");
        ReadOnlySpan<char> span = ms.AsSpan(1, 0);
        Assert.Equal(0, span.Length);
    }
}
