/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests;

public class OperatorTests
{
    // --- implicit conversion to string ---

    [Fact]
    public void ImplicitConversion_ToString_ReturnsContent()
    {
        MutableString ms = new("hello");
        string s = ms;
        Assert.Equal("hello", s);
    }

    // --- implicit conversion from string ---

    [Fact]
    public void ExplicitConversion_FromString_CreatesEquivalentMutableString()
    {
        MutableString ms = "hello";
        Assert.Equal("hello", ms.ToString());
    }

    // --- implicit conversion to ReadOnlySpan<char> ---

    [Fact]
    public void ImplicitConversion_ToSpan_ReturnsContent()
    {
        MutableString ms = new("hello");
        ReadOnlySpan<char> span = ms;
        Assert.Equal("hello", span);
    }

    // --- implicit conversion from ReadOnlySpan<char> ---

    [Fact]
    public void ExplicitConversion_FromSpan_CreatesEquivalentMutableString()
    {
        ReadOnlySpan<char> span = "hello".AsSpan();
        MutableString ms = span;
        Assert.Equal("hello", ms.ToString());
    }

    // --- implicit conversion to char[] ---

    [Fact]
    public void ImplicitConversion_ToArray_ReturnsContent()
    {
        MutableString ms = new("hello");
        string s = ms;
        Assert.Equal("hello", s);
    }

    // --- implicit conversion from char[] ---

    [Fact]
    public void ExplicitConversion_FromArray_CreatesEquivalentMutableString()
    {
        char[] array = [.. "hello"];
        MutableString ms = array;
        Assert.Equal("hello", ms.ToString());
    }

    // --- operator + (MutableString, MutableString) ---

    [Fact]
    public void AdditionOperator_TwoMutableStrings_ConcatenatesContent()
    {
        MutableString a = new("hello ");
        MutableString b = new("world");
        MutableString result = a + b;
        Assert.Equal("hello world", result.ToString());
    }

    [Fact]
    public void AdditionOperator_TwoMutableStrings_DoesNotModifyOperands()
    {
        MutableString a = new("hello ");
        MutableString b = new("world");
        _ = a + b;
        Assert.Equal("hello ", a.ToString());
        Assert.Equal("world", b.ToString());
    }

    [Fact]
    public void AdditionOperator_LeftNull_TreatsAsEmpty()
    {
        MutableString? a = null;
        MutableString b = new("world");
        MutableString result = a + b;
        Assert.Equal("world", result.ToString());
    }

    [Fact]
    public void AdditionOperator_RightNull_TreatsAsEmpty()
    {
        MutableString a = new("hello");
        MutableString? b = null;
        MutableString result = a + b;
        Assert.Equal("hello", result.ToString());
    }

    [Fact]
    public void AdditionOperator_BothNull_ProducesEmpty()
    {
        MutableString? a = null;
        MutableString? b = null;
        MutableString result = a + b;
        Assert.Equal("", result.ToString());
    }

    // --- operator + (MutableString, string) ---

    [Fact]
    public void AdditionOperator_MutableStringPlusString_ConcatenatesContent()
    {
        MutableString a = new("hello ");
        MutableString result = a + "world";
        Assert.Equal("hello world", result.ToString());
    }

    [Fact]
    public void AdditionOperator_MutableStringPlusNullString_TreatsAsEmpty()
    {
        MutableString a = new("hello");
        MutableString result = a + (string?)null;
        Assert.Equal("hello", result.ToString());
    }

    // --- operator + (string, MutableString) ---

    [Fact]
    public void AdditionOperator_StringPlusMutableString_ConcatenatesContent()
    {
        MutableString b = new("world");
        MutableString result = "hello " + b;
        Assert.Equal("hello world", result.ToString());
    }

    [Fact]
    public void AdditionOperator_NullStringPlusMutableString_TreatsAsEmpty()
    {
        MutableString b = new("world");
        MutableString result = (string?)null + b;
        Assert.Equal("world", result.ToString());
    }

    // --- += (desugars to operator +) ---

    [Fact]
    public void AdditionAssignment_ReassignsToNewInstance_DoesNotMutateOriginal()
    {
        MutableString a = new("hello ");
        MutableString original = a;

        a += "world";

        Assert.Equal("hello world", a.ToString());
        Assert.Equal("hello ", original.ToString()); // original instance untouched
        Assert.False(ReferenceEquals(a, original));  // 'a' now points at a new instance
    }

    // --- equality operators (==, !=) ---

    [Fact]
    public void EqualOperator_Compare()
    {
        MutableString a = new("apple");
        MutableString b = new("banana");
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact]
    public void EqualOperator_CompareNull()
    {
        MutableString a = new("apple");
        MutableString? b = null;
        Assert.False(a == b);
        Assert.True(a != b);
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact]
    public void EqualOperator_CompareBothNull()
    {
        MutableString? a = null;
        MutableString? b = null;
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.True(a == b);
        Assert.False(a != b);
    }

    // --- operator < ---

    [Fact]
    public void LessThanOperator_TrueWhenLess()
    {
        MutableString a = new("apple");
        MutableString b = new("banana");
        Assert.True(a < b);
        Assert.False(b < a);
    }

    [Fact]
    public void LessThanOperator_FalseWhenEqual()
    {
        MutableString a = new("same");
        MutableString b = new("same");
        Assert.False(a < b);
    }

    [Fact]
    public void LessThanOperator_NullIsLessThanNonNull()
    {
        MutableString? a = null;
        MutableString b = new("a");
        Assert.True(a < b);
        Assert.False(b < a);
    }

    [Fact]
    public void LessThanOperator_BothNull_IsFalse()
    {
        MutableString? a = null;
        MutableString? b = null;
        Assert.False(a < b);
    }

    // --- operator > ---

    [Fact]
    public void GreaterThanOperator_TrueWhenGreater()
    {
        MutableString a = new("banana");
        MutableString b = new("apple");
        Assert.True(a > b);
        Assert.False(b > a);
    }

    [Fact]
    public void GreaterThanOperator_NonNullIsGreaterThanNull()
    {
        MutableString a = new("a");
        MutableString? b = null;
        Assert.True(a > b);
        Assert.False(b > a);
    }

    [Fact]
    public void GreaterThanOperator_BothNull_IsFalse()
    {
        MutableString? a = null;
        MutableString? b = null;
        Assert.False(a > b);
    }

    // --- operator <= / >= ---

    [Fact]
    public void LessThanOrEqualOperator_TrueWhenEqual()
    {
        MutableString a = new("same");
        MutableString b = new("same");
        Assert.True(a <= b);
        Assert.True(a >= b);
    }

    [Fact]
    public void LessThanOrEqualOperator_BothNull_IsTrue()
    {
        MutableString? a = null;
        MutableString? b = null;
        Assert.True(a <= b);
        Assert.True(a >= b);
    }

    [Fact]
    public void GreaterThanOrEqualOperator_TrueWhenGreater()
    {
        MutableString a = new("banana");
        MutableString b = new("apple");
        Assert.True(a >= b);
        Assert.False(b >= a);
    }

    [Fact]
    public void LessThanOrEqualOperator_NullVsNonNull()
    {
        MutableString? a = null;
        MutableString b = new("a");
        Assert.True(a <= b);
        Assert.False(b <= a);
    }
}