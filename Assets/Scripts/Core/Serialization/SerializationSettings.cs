using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Serialization
{
    public static class SerializationSettings
    {
        public static bool UseChecksums { get; set; } = true;

        public static bool ChecksumValid { get; set; } = true;
    }
}