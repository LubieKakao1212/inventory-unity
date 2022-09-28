using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Serialization
{
    public interface ISerializable
    {
        /// <summary>
        /// Serializes the object to json
        /// </summary>
        JToken Serialize();
        
        /// <summary>
        /// Deserializes the object from json
        /// </summary>
        void Deserialize(JToken json);
    }
}
