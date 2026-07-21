/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests;

public class ToStringTests
{
    [Fact]
    public void ToString_ReflectsCurrentState_AfterMutation()
    {
        MutableString ms = new("hello");
        ms.Append(" world");
        Assert.Equal("hello world", ms.ToString());
    }

    [Fact]
    public void ToString_DoesNotIncludeUnusedBufferCapacity()
    {
#pragma warning disable IDE0017 // Simplify object initialization
        MutableString ms = new("hello world");
#pragma warning restore IDE0017 // Simplify object initialization
        ms.Length = 5; // shrink; Buffer retains extra capacity beyond Count
        Assert.Equal("hello", ms.ToString());
        Assert.Equal(5, ms.Length);
        Assert.Equal(5, ms.ToString().Length);
    }
}
