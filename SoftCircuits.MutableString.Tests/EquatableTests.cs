/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests
{
    public class EquatableTests
    {
        // --- Equals(MutableString?) ---

        [Fact]
        public void Equals_MutableString_SameContent_ReturnsTrue()
        {
            MutableString a = new("hello");
            MutableString b = new("hello");
            Assert.True(a.Equals(b));
        }

        [Fact]
        public void Equals_MutableString_DifferentContent_ReturnsFalse()
        {
            MutableString a = new("hello");
            MutableString b = new("world");
            Assert.False(a.Equals(b));
        }

        [Fact]
        public void Equals_MutableString_DifferentLength_ReturnsFalse()
        {
            MutableString a = new("hello");
            MutableString b = new("hello!");
            Assert.False(a.Equals(b));
        }

        [Fact]
        public void Equals_MutableString_Null_ReturnsFalse()
        {
            MutableString a = new("hello");
            Assert.False(a.Equals((MutableString?)null));
        }

        [Fact]
        public void Equals_MutableString_BothEmpty_ReturnsTrue()
        {
            MutableString a = new("");
            MutableString b = new("");
            Assert.True(a.Equals(b));
        }

        [Fact]
        public void Equals_MutableString_IsOrdinal_CaseSensitive()
        {
            MutableString a = new("Hello");
            MutableString b = new("hello");
            Assert.False(a.Equals(b));
        }

        [Fact]
        public void Equals_MutableString_SameInstance_ReturnsTrue()
        {
            MutableString a = new("hello");
            Assert.True(a.Equals(a));
        }

        [Fact]
        public void Equals_MutableString_IsSymmetric()
        {
            MutableString a = new("hello");
            MutableString b = new("hello");
            Assert.Equal(a.Equals(b), b.Equals(a));
        }

        [Fact]
        public void Equals_MutableString_OnlyUsesLogicalLength_NotBufferCapacity()
        {
            // Regression check: equality must respect Count, not leftover Buffer capacity.
            MutableString source = new("hello-extra");
            source.Length = 5; // logically "hello"

            MutableString target = new("hello");
            Assert.True(target.Equals(source));
            Assert.True(source.Equals(target));
        }

        // --- Equals(string?) ---

        [Fact]
        public void Equals_String_SameContent_ReturnsTrue()
        {
            MutableString a = new("hello");
            Assert.True(a.Equals("hello"));
        }

        [Fact]
        public void Equals_String_DifferentContent_ReturnsFalse()
        {
            MutableString a = new("hello");
            Assert.False(a.Equals("world"));
        }

        [Fact]
        public void Equals_String_DifferentLength_ReturnsFalse()
        {
            MutableString a = new("hello");
            Assert.False(a.Equals("hello!"));
        }

        [Fact]
        public void Equals_String_Null_ReturnsFalse()
        {
            MutableString a = new("hello");
            Assert.False(a.Equals((string?)null));
        }

        [Fact]
        public void Equals_String_EmptyVsEmpty_ReturnsTrue()
        {
            MutableString a = new("");
            Assert.True(a.Equals(""));
        }

        [Fact]
        public void Equals_String_IsOrdinal_CaseSensitive()
        {
            MutableString a = new("Hello");
            Assert.False(a.Equals("hello"));
        }

        // --- Equals(object?) ---

        [Fact]
        public void Equals_Object_Null_ReturnsFalse()
        {
            MutableString a = new("hello");
            Assert.False(a.Equals((object?)null));
        }

        [Fact]
        public void Equals_Object_BoxedMutableString_SameContent_ReturnsTrue()
        {
            MutableString a = new("hello");
            object b = new MutableString("hello");
            Assert.True(a.Equals(b));
        }

        [Fact]
        public void Equals_Object_BoxedMutableString_DifferentContent_ReturnsFalse()
        {
            MutableString a = new("hello");
            object b = new MutableString("world");
            Assert.False(a.Equals(b));
        }

        [Fact]
        public void Equals_Object_BoxedString_SameContent_ReturnsTrue()
        {
            MutableString a = new("hello");
            object b = "hello";
            Assert.True(a.Equals(b));
        }

        [Fact]
        public void Equals_Object_BoxedString_DifferentContent_ReturnsFalse()
        {
            MutableString a = new("hello");
            object b = "world";
            Assert.False(a.Equals(b));
        }

        [Fact]
        public void Equals_Object_UnsupportedType_ReturnsFalse()
        {
            MutableString a = new("hello");
            object b = 42;
            Assert.False(a.Equals(b));
        }

        // --- GetHashCode ---

        [Fact]
        public void GetHashCode_EqualContent_ProducesSameHash()
        {
            MutableString a = new("hello");
            MutableString b = new("hello");
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        [Fact]
        public void GetHashCode_MatchesEquivalentStringHash()
        {
            // Required for consistency with IEquatable<string>: if ms.Equals("hello") is true,
            // ms.GetHashCode() must equal "hello".GetHashCode() for hash-based collections
            // mixing MutableString and string keys to behave correctly.
            MutableString ms = new("hello");
            Assert.Equal("hello".GetHashCode(), ms.GetHashCode());
        }

        [Fact]
        public void GetHashCode_IsStableAcrossMultipleCalls()
        {
            MutableString ms = new("hello");
            int first = ms.GetHashCode();
            int second = ms.GetHashCode();
            Assert.Equal(first, second);
        }

        [Fact]
        public void GetHashCode_OnlyUsesLogicalLength_NotBufferCapacity()
        {
            MutableString source = new("hello-extra");
            source.Length = 5; // logically "hello"

            Assert.Equal("hello".GetHashCode(), source.GetHashCode());
        }

        [Fact]
        public void GetHashCode_DifferentContent_TypicallyProducesDifferentHash()
        {
            // Not a strict guarantee of hashing in general, but a reasonable sanity check
            // for distinct short strings under a well-behaved hash function.
            MutableString a = new("hello");
            MutableString b = new("world");
            Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        }

        // --- Consistency between Equals overloads ---

        [Theory]
        [InlineData("hello", "hello")]
        [InlineData("", "")]
        [InlineData("Case", "case")]
        public void Equals_MutableStringAndStringOverloads_AgreeOnSameContent(string a, string b)
        {
            MutableString ms = new(a);
            Assert.Equal(ms.Equals(new MutableString(b)), ms.Equals(b));
        }
    }
}
