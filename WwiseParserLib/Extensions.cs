using System;
using System.Collections.Generic;
using System.Net;

namespace WwiseParserLib
{
    /// <summary>
    /// Extensions for data conversion and representation.
    /// </summary>
    internal static class Extensions
    {
        private const string Blanks = "                                                            ";

        // note that these are in big endian, corresponding to hex editor
        private static Dictionary<uint, string> _hashPairs = new Dictionary<uint, string>()
        {
            { 0x37BCB7E2, "Master Audio Bus" },
            { 0xF76EFE2F, "Master Secondary Bus" }
        };

        internal static string GetSwappedUInt32(this byte[] bytes)
        {
            long value = BitConverter.ToUInt32(bytes, 0);
            value = IPAddress.NetworkToHostOrder(value);
            var id = (uint)(value >> 32);
            return _hashPairs.ContainsKey(id)
                ? id.ToString("X") + '_' + _hashPairs[id]
                : id.ToString("X");
        }

        internal static uint GetDecimalUInt32(this byte[] bytes)
            => BitConverter.ToUInt32(bytes, 0);

        public static string ToHex(this byte value)
            => value.ToString("x2");

        public static string ToHex(this ushort value)
            => value.ToString("x4");

        public static string ToHex(this uint value)
            => value.ToString("x8");

        public static string ToHex(this ulong value)
            => value.ToString("x16");

        public static string ToHex(this int value)
            => value.ToString("x8");

        public static string Indent(this string str, int count)
        {
            return Blanks.Substring(0, count) + str;
        }

        public static string ToTimeCode(this double ms)
        {
            var secs = (int)ms / 1000;
            var millis = (int)ms % 1000;
            return $"{(secs / 60).ToString("00")}:{(secs % 60).ToString("00")}.{millis.ToString("000")}";
        }
    }
}
