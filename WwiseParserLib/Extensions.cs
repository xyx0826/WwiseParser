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
    }
}
