/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests;

public class ConstructorTests
{
    [Fact]
    public void DefaultConstructor_IsEmpty()
    {
        MutableString ms = new();
        Assert.Equal(0, ms.Length);
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void CapacityConstructor_IsEmptyRegardlessOfCapacity()
    {
        MutableString ms = new(100);
        Assert.Equal(0, ms.Length);
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void CapacityConstructor_ZeroIsValid()
    {
        MutableString ms = new(0);
        Assert.Equal(0, ms.Length);
    }

    [Fact]
    public void CapacityConstructor_NegativeThrows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(-1));
    }

    [Fact]
    public void CopyConstructor_NullProducesEmpty()
    {
        MutableString ms = new((MutableString?)null);
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void CopyConstructor_CopiesContent()
    {
        MutableString original = new("hello");
        MutableString copy = new(original);
        Assert.Equal("hello", copy.ToString());
    }

    [Fact]
    public void CopyConstructor_HonorsCountNotBufferCapacity()
    {
        // Shrink after building with extra capacity in the buffer.
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString original = new("hello world");
#pragma warning restore IDE0017 // Simplify object initialization
        original.Length = 5; // "hello", but Buffer still has room for "hello world"

        MutableString copy = new(original);
        Assert.Equal("hello", copy.ToString());
    }

    [Fact]
    public void CopyConstructor_IsIndependentOfOriginal()
    {
        MutableString original = new("hello");
        MutableString copy = new(original);

        original.Append(" world");

        Assert.Equal("hello world", original.ToString());
        Assert.Equal("hello", copy.ToString());
    }

    [Fact]
    public void StringConstructor_NullProducesEmpty()
    {
        MutableString ms = new((string?)null);
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void StringConstructor_EmptyStringProducesEmpty()
    {
        MutableString ms = new("");
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void StringConstructor_CopiesContent()
    {
        MutableString ms = new("hello");
        Assert.Equal("hello", ms.ToString());
        Assert.Equal(5, ms.Length);
    }

    [Fact]
    public void CharArrayConstructor_NullProducesEmpty()
    {
        MutableString ms = new((char[]?)null);
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void CharArrayConstructor_CopiesContent()
    {
        MutableString ms = new(['h', 'e', 'l', 'l', 'o']);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void CharArrayConstructor_IsIndependentOfSourceArray()
    {
        char[] source = ['a', 'b', 'c'];
        MutableString ms = new(source);
        source[0] = 'z';
        Assert.Equal("abc", ms.ToString());
    }

    [Fact]
    public void CharArrayRangeConstructor_NullArrayHonorsRequestedLength()
    {
        MutableString ms = new(null, 0, 5);
        Assert.Equal(0, ms.Length);
    }

    [Fact]
    public void CharArrayRangeConstructor_NullArrayWithZeroLength()
    {
        MutableString ms = new(null, 0, 0);
        Assert.Equal(0, ms.Length);
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void CharArrayRangeConstructor_CopiesSubrange()
    {
        char[] array = ['a', 'b', 'c', 'd', 'e'];
        MutableString ms = new(array, 1, 3);
        Assert.Equal("bcd", ms.ToString());
    }

    [Fact]
    public void CharArrayRangeConstructor_NegativeLengthThrows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(['a', 'b'], 0, -1));
    }

    [Fact]
    public void CharArrayRangeConstructor_NegativeStartIndexThrows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(['a', 'b'], -1, 1));
    }

    [Fact]
    public void CharArrayRangeConstructor_RangeExceedingArrayThrows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(['a', 'b', 'c'], 1, 5));
    }

    [Fact]
    public void CharArrayRangeConstructor_ExactFitDoesNotThrow()
    {
        MutableString ms = new(['a', 'b', 'c'], 0, 3);
        Assert.Equal("abc", ms.ToString());
    }

    [Fact]
    public void SpanConstructor_EmptySpanProducesEmpty()
    {
        MutableString ms = new(ReadOnlySpan<char>.Empty);
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void SpanConstructor_CopiesContent()
    {
        ReadOnlySpan<char> span = "hello".AsSpan();
        MutableString ms = new(span);
        Assert.Equal("hello", ms.ToString());
    }
}
