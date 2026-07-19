namespace SoftCircuits.MutableString.Tests;

public class MoveTests
{
    [Fact]
    public void Move_Forward_OverwritesTargetRegion()
    {
        MutableString ms = new("abcdef");
        ms.Move(0, 2, 3); // copy "abc" over positions 2..4
        Assert.Equal("ababcf", ms.ToString());
    }

    [Fact]
    public void Move_Backward_OverwritesTargetRegion()
    {
        MutableString ms = new("abcdef");
        ms.Move(3, 0, 3); // copy "def" over positions 0..2
        Assert.Equal("defdef", ms.ToString());
    }

    [Fact]
    public void Move_CountExceedingAvailable_IsClampedBySourceBound()
    {
        MutableString ms = new("abcde");
        ms.Move(2, 0, 100); // only "cde" (3 chars) available from source index 2
        Assert.Equal("cdede", ms.ToString());
    }

    [Fact]
    public void Move_CountExceedingAvailable_IsClampedByTargetBound()
    {
        MutableString ms = new("abcde");
        ms.Move(0, 2, 100); // only 3 slots available at the target starting at index 2
        Assert.Equal("ababc", ms.ToString());
    }

    [Fact]
    public void Move_ZeroCount_IsNoOp()
    {
        MutableString ms = new("abcdef");
        ms.Move(0, 3, 0);
        Assert.Equal("abcdef", ms.ToString());
    }

    [Fact]
    public void Move_NegativeCount_IsNoOp()
    {
        MutableString ms = new("abcdef");
        ms.Move(0, 3, -5);
        Assert.Equal("abcdef", ms.ToString());
    }

    [Fact]
    public void Move_SourceEqualsTarget_IsEffectivelyNoOp()
    {
        MutableString ms = new("abcdef");
        ms.Move(1, 1, 3);
        Assert.Equal("abcdef", ms.ToString());
    }

    [Fact]
    public void Move_DoesNotChangeLength()
    {
        MutableString ms = new("abcdef");
        ms.Move(0, 2, 3);
        Assert.Equal(6, ms.Length);
    }
}
