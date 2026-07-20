using System.Collections;

namespace SoftCircuits.MutableString;

public sealed partial class MutableString : IEnumerable<char>
{
    private int EnumeratorVersion = 0;

    public IEnumerator<char> GetEnumerator()
    {
        int version = EnumeratorVersion;

        for (int i = 0; i < Count; i++)
        {
            if (version != EnumeratorVersion)
                throw new InvalidOperationException("MutableString was modified during enumeration.");

            yield return Buffer[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
