using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace WwiseParser
{
    static class Extensions
    {
        // note that these are in big endian, corresponding to hex editor
        private static Dictionary<uint, string> _hashPairs = new Dictionary<uint, string>()
        {
            { 0x37BCB7E2, "Master Audio Bus" },
            { 0xF76EFE2F, "Master Secondary Bus" }
        };

        public static bool HasSwitch(this string[] args, string name, string shortName = null)
            => args.Contains("--" + name) || (shortName == null ? false : args.Contains('-' + shortName));

        public static string GetSwappedUInt32(this byte[] bytes)
        {
            long value = BitConverter.ToUInt32(bytes);
            value = IPAddress.NetworkToHostOrder(value);
            var id = (uint)(value >> 32);
            return _hashPairs.ContainsKey(id)
                ? id.ToString("X") + '_' + _hashPairs[id]
                : id.ToString("X");
        }

        public static uint GetDecimalUInt32(this byte[] bytes)
            => BitConverter.ToUInt32(bytes);
    }
}
