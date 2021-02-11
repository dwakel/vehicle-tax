using HashidsNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTax.Services
{
    public static class IDService
    {
        private static readonly IHashids Hashids = new Hashids("9X~C2!D@zH)BTE(8P*VL&b7^kD#m%y", 10, "abcdefghijklmnopqrstuvwxyz123456789");

        public static string Encode(string prefix, long id)
        {
            var encoded = Hashids.EncodeLong(id);
            if (string.IsNullOrWhiteSpace(encoded))
            {
                throw new Exception("Encoded ID should not be empty");
            }

            return $"{prefix}_{encoded}";
        }

        public static long? Decode(string encoded)
        {
            if (string.IsNullOrWhiteSpace(encoded))
            {
                return null;
            }

            var id = encoded.Split('_').Last();
            var decoded = Hashids.DecodeLong(id).FirstOrDefault();

            return decoded != 0 ? decoded : (long?)null;
        }
    }
}
