namespace SoftCircuits.MutableString
{
    public sealed partial class MutableString : IComparable<MutableString>, IComparable<string>, IComparable
    {
        public int CompareTo(MutableString? other)
        {
            // By convention, any instance is "greater than" null
            if (other is null)
                return 1;
            return AsSpan().CompareTo(other.AsSpan(), StringComparison.Ordinal);
        }

        public int CompareTo(string? other)
        {
            // By convention, any instance is "greater than" null
            if (other is null)
                return 1;
            return AsSpan().CompareTo(other.AsSpan(), StringComparison.Ordinal);
        }

        public int CompareTo(object? obj)
        {
            return obj switch
            {
                null => 1,
                MutableString ms => CompareTo(ms),
                string s => CompareTo(s),
                _ => throw new ArgumentException($"Object must be of type {nameof(MutableString)} or {nameof(String)}.", nameof(obj))
            };
        }
    }
}
