using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace Core.Serialization.Tests
{
    public class Checksum
    {
        private JObject JObj = JObject.Parse(
                "{" +
                "   \"test\": 123," +
                "   \"A\": \"aBc\"," +
                "   \"x\": null" +
                "}");

        private JArray JArr = JArray.Parse(
                "[" +
                "   123," +
                "   \"aBc\"," +
                "   null" +
                "]");

        [Test]
        public void ValidChecksum()
        {
            SerializationSettings.UseChecksums = true;
            SerializationSettings.ChecksumValid = true;

            JObject jobj = (JObject)JObj.DeepClone(); 
            jobj.AppendChecksum();
            jobj.ValidateChecksum();
            Assert.IsTrue(SerializationSettings.ChecksumValid, "Valid Object Checksum");

            JArray jarr = (JArray)JArr.DeepClone();
            jarr.AppendChecksum();
            jarr.ValidateChecksum();
            Assert.IsTrue(SerializationSettings.ChecksumValid, "Valid Array Checksum");
        }

        [Test]
        public void ModifiedChecksums()
        {
            SerializationSettings.UseChecksums = true;
            SerializationSettings.ChecksumValid = true;

            JObject jobj = (JObject)JObj.DeepClone();
            jobj.AppendChecksum();
            jobj["aaa"] = "!!!";
            jobj.ValidateChecksum();
            Assert.IsFalse(SerializationSettings.ChecksumValid, "Valid Object Checksum");

            SerializationSettings.ChecksumValid = true;
            JArray jarr = (JArray)JArr.DeepClone();
            jarr.AppendChecksum();
            jarr[0] = 12;
            jarr.ValidateChecksum();
            Assert.IsFalse(SerializationSettings.ChecksumValid, "Valid Array Checksum");
        }

        [Test]
        public void NoChecksumSave()
        {
            SerializationSettings.UseChecksums = false;
            SerializationSettings.ChecksumValid = true;
            JObject jobj = (JObject)JObj.DeepClone();
            jobj.AppendChecksum();

            SerializationSettings.UseChecksums = true;
            jobj.ValidateChecksum();
            Assert.IsFalse(SerializationSettings.ChecksumValid, "Valid Object Checksum");
            
            SerializationSettings.UseChecksums = false;
            SerializationSettings.ChecksumValid = true;
            JArray jarr = (JArray)JArr.DeepClone();
            jarr.AppendChecksum();

            SerializationSettings.UseChecksums = true;
            jarr.ValidateChecksum();
            Assert.IsFalse(SerializationSettings.ChecksumValid, "Valid Array Checksum");
        }

        [Test]
        public void ChecksumDisabled()
        {
            throw new System.NotImplementedException();
        }
    }
}
