/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests;

public class AppendTests
{
    // --- string overload ---

    [Fact]
    public void Append_String_ToEmpty()
    {
        MutableString ms = new();
        ms.Append("hello");
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Append_String_ToNonEmpty()
    {
        MutableString ms = new("hello ");
        ms.Append("world");
        Assert.Equal("hello world", ms.ToString());
    }

    [Fact]
    public void Append_NullString_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Append((string?)null);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Append_EmptyString_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Append("");
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Append_Multiple_AccumulatesCorrectly()
    {
        MutableString ms = new();
        ms.Append("a");
        ms.Append("b");
        ms.Append("c");
        Assert.Equal("abc", ms.ToString());
        Assert.Equal(3, ms.Length);
    }

    [Fact]
    public void Append_TriggersGrowthAcrossMultipleReallocations()
    {
        MutableString ms = new();
        string chunk = new('x', 100);
        for (int i = 0; i < 10; i++)
            ms.Append(chunk);

        Assert.Equal(1000, ms.Length);
        Assert.Equal(new string('x', 1000), ms.ToString());
    }

    // --- char[] overload ---

    [Fact]
    public void Append_CharArray_ToNonEmpty()
    {
        MutableString ms = new("hello ");
        ms.Append(['w', 'o', 'r', 'l', 'd']);
        Assert.Equal("hello world", ms.ToString());
    }

    [Fact]
    public void Append_NullCharArray_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Append((char[]?)null);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Append_EmptyCharArray_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Append(Array.Empty<char>());
        Assert.Equal("hello", ms.ToString());
    }

    // --- ReadOnlySpan<char> overload ---

    [Fact]
    public void Append_Span_ToNonEmpty()
    {
        MutableString ms = new("hello ");
        ms.Append("world".AsSpan());
        Assert.Equal("hello world", ms.ToString());
    }

    [Fact]
    public void Append_EmptySpan_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Append(ReadOnlySpan<char>.Empty);
        Assert.Equal("hello", ms.ToString());
    }

    // --- MutableString overload ---

    [Fact]
    public void Append_MutableString_ToNonEmpty()
    {
        MutableString ms = new("hello ");
        ms.Append(new MutableString("world"));
        Assert.Equal("hello world", ms.ToString());
    }

    [Fact]
    public void Append_NullMutableString_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Append((MutableString?)null);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Append_MutableString_OnlyAppendsLogicalLength_NotBufferCapacity()
    {
        // Regression test: appending a MutableString must respect its Count, not the
        // full (possibly larger) capacity of its internal Buffer.
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString source = new("hello world");
#pragma warning restore IDE0017 // Simplify object initialization
        source.Length = 5; // logically "hello", but Buffer still holds "hello world..."

        MutableString ms = new();
        ms.Append(source);

        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Append_MutableString_ToItself()
    {
        MutableString ms = new("ab");
        ms.Append(ms);
        Assert.Equal("abab", ms.ToString());
    }
}
