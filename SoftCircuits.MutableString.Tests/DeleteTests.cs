namespace SoftCircuits.MutableString.Tests;

public class DeleteTests
{
    [Fact]
    public void Delete_FromMiddle()
    {
        MutableString ms = new("hello world");
        ms.Delete(5, 1); // remove the space
        Assert.Equal("helloworld", ms.ToString());
    }

    [Fact]
    public void Delete_FromStart()
    {
        MutableString ms = new("hello world");
        ms.Delete(0, 6);
        Assert.Equal("world", ms.ToString());
    }

    [Fact]
    public void Delete_ToEnd()
    {
        MutableString ms = new("hello world");
        ms.Delete(5, 6);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Delete_EntireString()
    {
        MutableString ms = new("hello");
        ms.Delete(0, 5);
        Assert.Equal("", ms.ToString());
        Assert.Equal(0, ms.Length);
    }

    [Fact]
    public void Delete_CountExceedingAvailable_IsClamped()
    {
        MutableString ms = new("hello");
        ms.Delete(2, 1000);
        Assert.Equal("he", ms.ToString());
    }

    [Fact]
    public void Delete_ZeroCount_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Delete(2, 0);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Delete_NegativeCount_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Delete(2, -5);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Delete_NegativeIndex_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Delete(-1, 2);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Delete_IndexEqualToLength_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Delete(5, 1);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Delete_IndexBeyondLength_IsNoOp()
    {
        MutableString ms = new("hello");
        ms.Delete(1000, 1);
        Assert.Equal("hello", ms.ToString());
    }

    [Fact]
    public void Delete_OnEmptyString_IsNoOp()
    {
        MutableString ms = new();
        ms.Delete(0, 1);
        Assert.Equal("", ms.ToString());
    }

    [Fact]
    public void Delete_SingleCharacter_PreservesSurroundingText()
    {
        MutableString ms = new("0123456789");
        ms.Delete(4, 1);
        Assert.Equal("012356789", ms.ToString());
    }

    [Fact]
    public void Delete_UpdatesLengthCorrectly()
    {
        MutableString ms = new("hello world");
        ms.Delete(5, 6);
        Assert.Equal(5, ms.Length);
    }
}
