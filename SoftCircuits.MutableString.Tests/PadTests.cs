namespace SoftCircuits.MutableString.Tests
{
    public class PadTests
    {
        [Fact]
        public void PadLeft_None()
        {
            MutableString ms = new("12345");
            ms.PadLeft(5);
            Assert.Equal("12345", ms.ToString());
        }

        [Fact]
        public void PadLeft_Negative()
        {
            MutableString ms = new("12345");
            ms.PadLeft(4);
            Assert.Equal("12345", ms.ToString());
        }

        [Fact]
        public void PadLeft_Double()
        {
            MutableString ms = new("12345");
            ms.PadLeft(10);
            Assert.Equal("     12345", ms.ToString());
        }

        [Fact]
        public void PadLeft_DoubleChar()
        {
            MutableString ms = new("12345");
            ms.PadLeft(10, '*');
            Assert.Equal("*****12345", ms.ToString());
        }

        [Fact]
        public void PadRight_None()
        {
            MutableString ms = new("12345");
            ms.PadRight(5);
            Assert.Equal("12345", ms.ToString());
        }

        [Fact]
        public void PadRight_Negative()
        {
            MutableString ms = new("12345");
            ms.PadRight(4);
            Assert.Equal("12345", ms.ToString());
        }

        [Fact]
        public void PadRight_Double()
        {
            MutableString ms = new("12345");
            ms.PadRight(10);
            Assert.Equal("12345     ", ms.ToString());
        }

        [Fact]
        public void PadRight_DoubleChar()
        {
            MutableString ms = new("12345");
            ms.PadRight(10, '*');
            Assert.Equal("12345*****", ms.ToString());
        }
    }
}
