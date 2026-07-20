/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests;

public class InsertTests
{
    [Fact]
    public void Insert_AtStart()
    {
        MutableString ms = new("world");
        ms.Insert(0, "hello ");
        Assert.Equal("hello world", ms.ToString());
    }

    [Fact]
    public void Insert_AtEnd_BehavesLikeAppend()
    {
        MutableString ms = new("hello");
        ms.Insert(5, " world");
        Assert.Equal("hello world", ms.ToString());
    }

    [Fact]
    public void Insert_InMiddle()
    {
        MutableString ms = new("helloworld");
        ms.Insert(5, " ");
        Assert.Equal("hello world", ms.ToString());
    }

    [Fact]
    public void Insert_IntoEmptyString()
    {
        MutableString ms = new();
        ms.Insert(0, "hello");
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Insert_IndexBeyondLength_ClampsToEnd()
    {
        MutableString ms = new("hello");
        ms.Insert(1000, "!");
        Assert.Equal("hello!", ms.ToString());
    }

    [Fact]
    public void Insert_NegativeIndex_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Insert(-1, "x");
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Insert_NullString_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Insert(2, (string?)null);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Insert_EmptyString_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Insert(2, "");
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Insert_CharArray_InMiddle()
    {
        MutableString ms = new("helloworld");
        ms.Insert(5, [' ']);
        Assert.Equal("hello world", ms.ToString());
    }

    [Fact]
    public void Insert_NullCharArray_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Insert(2, (char[]?)null);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Insert_Span_InMiddle()
    {
        MutableString ms = new("helloworld");
        ms.Insert(5, " ".AsSpan());
        Assert.Equal("hello world", ms.ToString());
    }

    [Fact]
    public void Insert_MutableString_InMiddle()
    {
        MutableString ms = new("helloworld");
        ms.Insert(5, new MutableString(" "));
        Assert.Equal("hello world", ms.ToString());
    }

    [Fact]
    public void Insert_NullMutableString_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Insert(2, (MutableString?)null);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Insert_MutableString_OnlyInsertsLogicalLength()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString source = new("XY-extra");
#pragma warning restore IDE0017 // Simplify object initialization
        source.Length = 2; // logically "XY"

        MutableString ms = new("ab");
        ms.Insert(1, source);

        Assert.Equal("aXYb", ms.ToString());
    }

    [Fact]
    public void Insert_LengthIsUpdatedCorrectly()
    {
        MutableString ms = new("abc");
        ms.Insert(1, "XYZ");
        Assert.Equal(6, ms.Length);
        Assert.Equal("aXYZbc", ms.ToString());
    }

    [Fact]
    public void Insert_PreservesCharactersAfterInsertionPoint()
    {
        MutableString ms = new("0123456789");
        ms.Insert(3, "---");
        Assert.Equal("012---3456789", ms.ToString());
    }
}
