/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString
{
    public sealed partial class MutableString : IComparable<MutableString>, IComparable<string>, IComparable
    {
        /// <summary>
        /// Compares this instance to the specified <see cref="MutableString"/> and returns an integer that indicates
        /// their relative position in the sort order.
        /// </summary>
        /// <param name="other">The value to compare with the source span.</param>
        /// <returns>
        /// A signed integer that indicates the relative order of this instance and <paramref name="other"/>.
        /// <list type="bullet">
        /// <item>If less than 0, this instance comes first.</item>
        /// <item>If 0, both items are equal.</item>
        /// <item>If greater than 0, this instance comes last.</item>
        /// </list>
        /// </returns>
        public int CompareTo(MutableString? other)
        {
            // By convention, any instance is "greater than" null
            if (other is null)
                return 1;
            return AsSpan().CompareTo(other.AsSpan(), StringComparison.Ordinal);
        }

        /// <summary>
        /// Compares this instance to the specified <see cref="string"/> and returns an integer that indicates
        /// their relative position in the sort order.
        /// </summary>
        /// <param name="other">The value to compare with the source span.</param>
        /// <returns>
        /// A signed integer that indicates the relative order of this instance and <paramref name="other"/>.
        /// <list type="bullet">
        /// <item>If less than 0, this instance comes first.</item>
        /// <item>If 0, both items are equal.</item>
        /// <item>If greater than 0, this instance comes last.</item>
        /// </list>
        /// </returns>
        public int CompareTo(string? other)
        {
            // By convention, any instance is "greater than" null
            if (other is null)
                return 1;
            return AsSpan().CompareTo(other.AsSpan(), StringComparison.Ordinal);
        }

        /// <summary>
        /// Compares this instance to the specified <see cref="object"/> and returns an integer that indicates
        /// their relative position in the sort order.
        /// </summary>
        /// <param name="obj">The value to compare with the source span.</param>
        /// <returns>
        /// A signed integer that indicates the relative order of this instance and <paramref name="obj"/>.
        /// <list type="bullet">
        /// <item>If less than 0, this instance comes first.</item>
        /// <item>If 0, both items are equal.</item>
        /// <item>If greater than 0, this instance comes last.</item>
        /// </list>
        /// </returns>
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
