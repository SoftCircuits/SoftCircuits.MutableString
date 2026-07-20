using System.Collections;

namespace SoftCircuits.MutableString.Tests;

public class EnumerableTests
{
    [Fact]
    public void GetEnumerator_EnumeratesAllCharactersInOrder()
    {
        MutableString ms = new("hello");
        List<char> result = [];

        foreach (char c in ms)
            result.Add(c);

        Assert.Equal(['h', 'e', 'l', 'l', 'o'], result);
    }

    [Fact]
    public void GetEnumerator_EmptyString_ProducesNoElements()
    {
        MutableString ms = new();
        List<char> result = [];

        foreach (char c in ms)
            result.Add(c);

        Assert.Empty(result);
    }

    [Fact]
    public void GetEnumerator_OnlyEnumeratesLogicalLength_NotBufferCapacity()
    {
        // Regression check: must stop at Count, not walk into unused Buffer capacity.
        MutableString ms = new("hello world");
        ms.Length = 5; // logically "hello"; Buffer retains extra capacity

        List<char> result = [];
        foreach (char c in ms)
            result.Add(c);

        Assert.Equal(['h', 'e', 'l', 'l', 'o'], result);
    }

    [Fact]
    public void GetEnumerator_ManualMoveNext_ReturnsCharactersInOrder()
    {
        MutableString ms = new("ab");
        using IEnumerator<char> enumerator = ms.GetEnumerator();

        Assert.True(enumerator.MoveNext());
        Assert.Equal('a', enumerator.Current);

        Assert.True(enumerator.MoveNext());
        Assert.Equal('b', enumerator.Current);

        Assert.False(enumerator.MoveNext());
    }

    [Fact]
    public void GetEnumerator_NonGenericInterface_EnumeratesCorrectly()
    {
        IEnumerable ms = new MutableString("hi");
        List<object?> result = [];

        foreach (object? c in ms)
            result.Add(c);

        Assert.Equal(['h', 'i'], result);
    }

    [Fact]
    public void GetEnumerator_TwoIndependentEnumerators_DoNotInterfere()
    {
        MutableString ms = new("abc");

        using IEnumerator<char> first = ms.GetEnumerator();
        using IEnumerator<char> second = ms.GetEnumerator();

        Assert.True(first.MoveNext());
        Assert.Equal('a', first.Current);

        Assert.True(second.MoveNext());
        Assert.Equal('a', second.Current);

        Assert.True(first.MoveNext());
        Assert.Equal('b', first.Current);

        Assert.True(second.MoveNext());
        Assert.Equal('b', second.Current);
    }

    [Fact]
    public void GetEnumerator_ThrowsOnModificationDuringEnumeration()
    {
        MutableString ms = new("hello");
        IEnumerator<char> enumerator = ms.GetEnumerator();

        Assert.True(enumerator.MoveNext()); // enumeration now in progress

        ms.Append("!"); // triggers Resize -> bumps _version

        Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
    }

    [Fact]
    public void GetEnumerator_ThrowsOnModification_ViaForeach()
    {
        MutableString ms = new("hello");

        Assert.Throws<InvalidOperationException>(() =>
        {
            foreach (char c in ms)
            {
                if (c == 'e')
                    ms.Delete(0, 1); // triggers Resize mid-enumeration
            }
        });
    }

    [Fact]
    public void GetEnumerator_ModificationAfterEnumerationCompletes_DoesNotThrow()
    {
        MutableString ms = new("ab");
        IEnumerator<char> enumerator = ms.GetEnumerator();

        while (enumerator.MoveNext())
        {
            // drain to completion
        }

        // Enumeration already finished; mutating now should have no effect on it.
        Exception? exception = Record.Exception(() => ms.Append("c"));
        Assert.Null(exception);
    }

    [Fact]
    public void GetEnumerator_ModificationThatDoesNotChangeCount_DoesNotThrow()
    {
        // Move/Copy overwrite content without touching Count, so they should not
        // trip the modification guard (matches List<T>'s own behavior: only
        // structural/length changes are tracked, not in-place value changes).
        MutableString ms = new("hello");
        IEnumerator<char> enumerator = ms.GetEnumerator();

        Assert.True(enumerator.MoveNext());

        ms.Copy("X", 0); // overwrites first character in place; Count unchanged

        Exception? exception = Record.Exception(() => enumerator.MoveNext());
        Assert.Null(exception);
    }
}
