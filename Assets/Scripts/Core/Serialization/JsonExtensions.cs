using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Serialization
{
    public static class JsonExtensions
    {
        private const string checksumFieldName = "chks";
        
        public static int GetChecksum(this JToken token)
        {
            return token.ToString(Formatting.None).GetHashCode(StringComparison.Ordinal);
        }

        public static void AppendChecksum(this JObject token)
        {
            if (SerializationSettings.UseChecksums)
            {
                int chks = token.GetChecksum();
                token[checksumFieldName] = chks;
            }
        }

        public static void AppendChecksum(this JArray token)
        {
            if (SerializationSettings.UseChecksums)
            {
                int chks = token.GetChecksum();
                token.Add(chks);
            }
        }

        public static void ValidateChecksum(this JObject token)
        {
            if (SerializationSettings.UseChecksums && SerializationSettings.ChecksumValid)
            { 
                var checksum = (int?)token[checksumFieldName];
                token.Remove(checksumFieldName);
                if (!checksum.HasValue || checksum.Value != token.GetChecksum())
                {
                    SerializationSettings.ChecksumValid = false;
                }
            }
        }

        public static void ValidateChecksum(this JArray token)
        {
            if (SerializationSettings.UseChecksums && SerializationSettings.ChecksumValid)
            {
                var checksum = (int?)token.Last;
                token.Remove(token.Last);
                if (!checksum.HasValue || checksum.Value != token.GetChecksum())
                {
                    SerializationSettings.ChecksumValid = false;
                }
            }
        }
    }
}