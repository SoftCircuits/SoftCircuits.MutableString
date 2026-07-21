/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString
{
    public sealed partial class MutableString
    {
        /// <summary>
        /// Implicitly converts a <see cref="MutableString"/> to <see cref="string"/>.
        /// </summary>
        public static implicit operator string(MutableString ms) => ms.ToString();

        /// <summary>
        /// Implicitly converts a <see cref="string"/> to <see cref="MutableString"/>.
        /// </summary>
        public static implicit operator MutableString(string s) => new(s);

        /// <summary>
        /// Implements <c>+</c> operator for two <see cref="MutableString"/>s.
        /// </summary>
        public static MutableString operator +(MutableString? left, MutableString? right)
        {
            MutableString result = new(left);
            result.Append(right);
            return result;
        }

        /// <summary>
        /// Implements <c>+</c> operator for a <see cref="MutableString"/> and <see cref="string"/>.
        /// </summary>
        public static MutableString operator +(MutableString? left, string? right)
        {
            MutableString result = new(left);
            result.Append(right);
            return result;
        }

        /// <summary>
        /// Implements <c>+</c> operator for a <see cref="string"/> and <see cref="MutableString"/>.
        /// </summary>
        public static MutableString operator +(string? left, MutableString? right)
        {
            MutableString result = new(left);
            result.Append(right);
            return result;
        }

        /// <summary>
        /// Implements <c>&lt;</c> operator for two <see cref="MutableString"/>s.
        /// </summary>
        public static bool operator <(MutableString? left, MutableString? right)
        {
            if (left is null)
                return right is not null;
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements <c>&gt;</c> operator for two <see cref="MutableString"/>s.
        /// </summary>
        public static bool operator >(MutableString? left, MutableString? right) => right < left;

        /// <summary>
        /// Implements <c>&lt;=</c> operator for two <see cref="MutableString"/>s.
        /// </summary>
        public static bool operator <=(MutableString? left, MutableString? right) => !(left > right);

        /// <summary>
        /// Implements <c>&gt;=</c> operator for two <see cref="MutableString"/>s.
        /// </summary>
        public static bool operator >=(MutableString? left, MutableString? right) => !(left < right);
    }
}
