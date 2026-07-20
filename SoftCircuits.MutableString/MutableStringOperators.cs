namespace SoftCircuits.MutableString
{
    public sealed partial class MutableString
    {
        /// <summary>
        /// Implicitly converts a <see cref="MutableString"/> to a <see cref="String"/>.
        /// </summary>
        /// <param name="ms">The mutable string to convert.</param>
        /// <returns>The converted string.</returns>
        public static implicit operator string(MutableString ms) => ms.ToString();

        /// <summary>
        /// Explicitly converts a <see cref="String"/> to a <see cref="MutableString"/>.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>The converted mutable string.</returns>
        public static explicit operator MutableString(string s) => new(s);

        public static MutableString operator +(MutableString? left, MutableString? right)
        {
            MutableString result = new(left);
            result.Append(right);
            return result;
        }

        public static MutableString operator +(MutableString? left, string? right)
        {
            MutableString result = new(left);
            result.Append(right);
            return result;
        }

        public static MutableString operator +(string? left, MutableString? right)
        {
            MutableString result = new(left);
            result.Append(right);
            return result;
        }

        public static bool operator <(MutableString? left, MutableString? right)
            => left is null ? right is not null : left.CompareTo(right) < 0;

        public static bool operator >(MutableString? left, MutableString? right)
            => right < left;

        public static bool operator <=(MutableString? left, MutableString? right)
            => !(left > right);

        public static bool operator >=(MutableString? left, MutableString? right)
            => !(left < right);

    }
}
