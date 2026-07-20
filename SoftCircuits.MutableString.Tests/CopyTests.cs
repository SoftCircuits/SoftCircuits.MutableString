/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests;

public class CopyTests
{
    [Fact]
    public void Copy_String_OverwritesInPlace_DoesNotGrow()
    {
        MutableString ms = new("hello world");
        ms.Copy("EARTH", 6);
        Assert.Equal("hello EARTH", ms.ToString());
        Assert.Equal(11, ms.Length); // unchanged
    }

    [Fact]
    public void Copy_String_AtStart()
    {
        MutableString ms = new("hello world");
        ms.Copy("HELLO", 0);
        Assert.Equal("HELLO world", ms.ToString());
    }

    [Fact]
    public void Copy_String_ExceedingRemainingSpace_IsTruncated()
    {
        MutableString ms = new("hello");
        ms.Copy("XYZ123", 3); // only 2 slots remain (indices 3,4)
        Assert.Equal("helXY", ms.ToString());
        Assert.Equal(5, ms.Length); // does not grow
    }

    [Fact]
    public void Copy_String_TargetIndexAtEnd_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Copy("X", 5);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Copy_String_TargetIndexBeyondEnd_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Copy("X", 100);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Copy_NullString_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Copy((string?)null, 1);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Copy_EmptyString_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Copy("", 1);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Copy_CharArray_Works()
    {
        MutableString ms = new("hello world");
        ms.Copy(['E', 'A', 'R', 'T', 'H'], 6);
        Assert.Equal("hello EARTH", ms.ToString());
    }

    [Fact]
    public void Copy_NullCharArray_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Copy((char[]?)null, 1);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Copy_Span_Works()
    {
        MutableString ms = new("hello world");
        ms.Copy("EARTH".AsSpan(), 6);
        Assert.Equal("hello EARTH", ms.ToString());
    }

    [Fact]
    public void Copy_MutableString_Works()
    {
        MutableString ms = new("hello world");
        ms.Copy(new MutableString("EARTH"), 6);
        Assert.Equal("hello EARTH", ms.ToString());
    }

    [Fact]
    public void Copy_NullMutableString_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Copy((MutableString?)null, 1);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Copy_MutableString_OnlyUsesLogicalLength()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString source = new("EARTH-extra");
#pragma warning restore IDE0017 // Simplify object initialization
        source.Length = 5; // logically "EARTH"

        MutableString ms = new("hello world");
        ms.Copy(source, 6);

        Assert.Equal("hello EARTH", ms.ToString());
    }
}
