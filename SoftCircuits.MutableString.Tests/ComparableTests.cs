namespace SoftCircuits.MutableString.Tests;

public class ComparableTests
{

    // --- CompareTo(MutableString?) ---

    [Fact]
    public void CompareTo_MutableString_EqualContent_ReturnsZero()
    {
        MutableString a = new("hello");
        MutableString b = new("hello");
        Assert.Equal(0, a.CompareTo(b));
    }

    [Fact]
    public void CompareTo_MutableString_Less_ReturnsNegative()
    {
        MutableString a = new("apple");
        MutableString b = new("banana");
        Assert.True(a.CompareTo(b) < 0);
    }

    [Fact]
    public void CompareTo_MutableString_Greater_ReturnsPositive()
    {
        MutableString a = new("banana");
        MutableString b = new("apple");
        Assert.True(a.CompareTo(b) > 0);
    }

    [Fact]
    public void CompareTo_MutableString_Null_ReturnsPositive()
    {
        MutableString a = new("hello");
        Assert.True(a.CompareTo((MutableString?)null) > 0);
    }

    [Fact]
    public void CompareTo_MutableString_IsOrdinal_CaseSensitive()
    {
        // Ordinal: uppercase letters sort before lowercase ('A' = 65 < 'a' = 97)
        MutableString upper = new("Apple");
        MutableString lower = new("apple");
        Assert.True(upper.CompareTo(lower) < 0);
    }

    [Fact]
    public void CompareTo_MutableString_ShorterPrefix_IsLess()
    {
        MutableString shorter = new("abc");
        MutableString longer = new("abcd");
        Assert.True(shorter.CompareTo(longer) < 0);
        Assert.True(longer.CompareTo(shorter) > 0);
    }

    [Fact]
    public void CompareTo_MutableString_EmptyVsNonEmpty()
    {
        MutableString empty = new("");
        MutableString nonEmpty = new("a");
        Assert.True(empty.CompareTo(nonEmpty) < 0);
    }

    [Fact]
    public void CompareTo_MutableString_BothEmpty_ReturnsZero()
    {
        MutableString a = new("");
        MutableString b = new("");
        Assert.Equal(0, a.CompareTo(b));
    }

    [Fact]
    public void CompareTo_MutableString_OnlyUsesLogicalLength()
    {
        // Regression check: comparison must respect Count, not Buffer capacity.
        MutableString source = new("apple-extra");
        source.Length = 5; // logically "apple"

        MutableString target = new("apple");
        Assert.Equal(0, target.CompareTo(source));
    }

    // --- CompareTo(string?) ---

    [Fact]
    public void CompareTo_String_EqualContent_ReturnsZero()
    {
        MutableString a = new("hello");
        Assert.Equal(0, a.CompareTo("hello"));
    }

    [Fact]
    public void CompareTo_String_Less_ReturnsNegative()
    {
        MutableString a = new("apple");
        Assert.True(a.CompareTo("banana") < 0);
    }

    [Fact]
    public void CompareTo_String_Greater_ReturnsPositive()
    {
        MutableString a = new("banana");
        Assert.True(a.CompareTo("apple") > 0);
    }

    [Fact]
    public void CompareTo_String_Null_ReturnsPositive()
    {
        MutableString a = new("hello");
        Assert.True(a.CompareTo((string?)null) > 0);
    }

    // --- CompareTo(object?) ---

    [Fact]
    public void CompareTo_Object_Null_ReturnsPositive()
    {
        MutableString a = new("hello");
        Assert.True(a.CompareTo((object?)null) > 0);
    }

    [Fact]
    public void CompareTo_Object_BoxedMutableString_DelegatesCorrectly()
    {
        MutableString a = new("apple");
        object b = new MutableString("banana");
        Assert.True(a.CompareTo(b) < 0);
    }

    [Fact]
    public void CompareTo_Object_BoxedString_DelegatesCorrectly()
    {
        MutableString a = new("apple");
        object b = "banana";
        Assert.True(a.CompareTo(b) < 0);
    }

    [Fact]
    public void CompareTo_Object_UnsupportedType_Throws()
    {
        MutableString a = new("hello");
        Assert.Throws<ArgumentException>(() => a.CompareTo(42));
    }

    // --- Consistency with Equals ---

    [Theory]
    [InlineData("hello", "hello")]
    [InlineData("", "")]
    [InlineData("Case", "Case")]
    public void CompareTo_ReturnsZero_WhenEqualsReturnsTrue(string x, string y)
    {
        MutableString a = new(x);
        MutableString b = new(y);
        Assert.Equal(a.Equals(b), a.CompareTo(b) == 0);
    }

    [Fact]
    public void CompareTo_IsAntisymmetric()
    {
        MutableString a = new("apple");
        MutableString b = new("banana");

        int forward = a.CompareTo(b);
        int backward = b.CompareTo(a);

        Assert.Equal(Math.Sign(forward), -Math.Sign(backward));
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
}
