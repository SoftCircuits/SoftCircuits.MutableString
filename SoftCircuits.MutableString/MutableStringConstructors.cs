/////////////////////////////////////////////////////////////////////
// Copyright (c) 2026 Jonathan Wood

namespace SoftCircuits.MutableString
{
    public partial class MutableString
    {
        /// <summary>
        /// Constructs an empty <see cref="MutableString"/> instance.
        /// </summary>
        public MutableString()
        {
            Resize(0);
        }

        /// <summary>
        /// Constructs an empty <see cref="MutableString"/> instance.
        /// </summary>
        /// <param name="capacity">The initial capacity of the string.</param>
        public MutableString(int capacity)
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNegative(capacity);
#else
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));
#endif

            Resize(capacity);
            Count = 0;
        }

        /// <summary>
        /// Constructs an empty <see cref="MutableString"/> instance.
        /// </summary>
        /// <param name="value">Initial character values.</param>
        public MutableString(MutableString? value)
        {
            if (value == null)
            {
                Resize(0);
            }
            else
            {
                Resize(value.Length);
                Array.Copy(value.Buffer, Buffer, value.Length);
            }
        }

        /// <summary>
        /// Constructs a new <see cref="MutableString"/> instance.
        /// </summary>
        /// <param name="s">Initial character values.</param>
        public MutableString(string? s)
        {
            if (s == null)
            {
                Resize(0);
            }
            else
            {
                Resize(s.Length);
                s.CopyTo(0, Buffer, 0, s.Length);
            }
        }

        /// <summary>
        /// Constructs a new <see cref="MutableString"/> instance.
        /// </summary>
        /// <param name="array">Initial character values.</param>
        public MutableString(char[]? array)
        {
            if (array == null)
            {
                Resize(0);
            }
            else
            {
                Resize(array.Length);
                Array.Copy(array, Buffer, array.Length);
            }
        }

        /// <summary>
        /// Constructs a new <see cref="MutableString"/> instance.
        /// </summary>
        /// <param name="array">Array with initial character values.</param>
        /// <param name="startIndex">Index of starting character to copy.</param>
        /// <param name="length">Number of characters to copy.</param>
        public MutableString(char[]? array, int startIndex, int length)
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNegative(length);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
#else
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
#endif

            if (array == null)
            {
                Resize(0);
            }
            else
            {
                if (startIndex + length > array.Length)
                    throw new ArgumentOutOfRangeException(nameof(length));

                Resize(length);
                Array.Copy(array, startIndex, Buffer, 0, length);
            }
        }

        /// <summary>
        /// Constructs a new <see cref="MutableString"/> instance.
        /// </summary>
        /// <param name="span">Initial character values.</param>
        public MutableString(ReadOnlySpan<char> span)
        {
            Resize(span.Length);
            span.CopyTo(Buffer);
        }
    }
}
