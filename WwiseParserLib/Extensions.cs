namespace WwiseParserLib
{
    /// <summary>
    /// Extensions for data conversion and representation.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// A bunch of whitespaces.
        /// </summary>
        private const string Blanks = "                                                            ";

        /// <summary>
        /// Converts the value to a 8-character lowercase hex representation.
        /// </summary>
        /// <param name="value">The value to be converted.</param>
        /// <returns>The hexadecimal representation.</returns>
        public static string ToHex(this uint value)
            => value.ToString("x8");

        /// <summary>
        /// Indents the string by the specified length.
        /// </summary>
        /// <param name="str">The string to be indented.</param>
        /// <param name="count">The length of the indentation.</param>
        /// <returns>The indented string.</returns>
        public static string Indent(this string str, int count)
        {
            return Blanks.Substring(0, count) + str;
        }

        /// <summary>
        /// Indents the multi-line string by the specified length.
        /// </summary>
        /// <param name="str">The string to be indented.</param>
        /// <param name="count">The length of the indentation.</param>
        /// <returns>The indented string.</returns>
        public static string IndentLines(this string str, int count)
        {
            var indentation = "".Indent(count);
            // No newline before the first line
            return indentation + str.Replace("\n", '\n' + indentation);
        }

        /// <summary>
        /// Converts the milliseconds duration to timecode.
        /// Format: mm:ss.0ms
        /// </summary>
        /// <param name="ms">The time duration in milliseconds.</param>
        /// <returns>The converted timecode.</returns>
        public static string ToTimeCode(this double ms)
        {
            bool minus = false;
            if (ms < 0)
            {
                ms = -ms;
                minus = true;
            }
            var secs = (int)ms / 1000;
            var millis = (int)ms % 1000;
            var str = $"{secs / 60:00}:{secs % 60:00}.{millis:000}";
            return minus ? '-' + str : str;
        }
    }
}
