/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

using System.Collections;

namespace SoftCircuits.MutableString;

public sealed partial class MutableString : IEnumerable<char>
{
    private int EnumeratorVersion = 0;

    /// <summary>
    /// Returns an enumerator that iterates the characters in this <see cref="MutableString"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public IEnumerator<char> GetEnumerator()
    {
        int version = EnumeratorVersion;

        for (int i = 0; i < InternalLength; i++)
        {
            // Check guard for changes to MutableString during enumeration
            if (version != EnumeratorVersion)
                throw new InvalidOperationException("MutableString was modified during enumeration.");

            yield return Buffer[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
