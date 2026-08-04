/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString.Tests
{
#pragma warning disable IDE1006 // Naming Styles
    public class _CodeExample
#pragma warning restore IDE1006 // Naming Styles
    {
        /// <summary>
        /// README.md code example.
        /// </summary>
        [Fact]
        public void CodeExample()
        {
            MutableString ms = "Test!";      // Test!
            ms.Insert(4, " this");           // Test this!
            ms.Copy(5, 0, 4);                // this this!
            ms.Replace(5, "test");           // this test!
            ms[0] = 'T';                     // This test!
            ms.Insert(4, " is a");           // This is a test!
        }
    }
}
