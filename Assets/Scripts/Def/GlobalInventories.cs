using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Inventory.Definition
{
    using Inv;
    using Api;

    public class GlobalInventories : MonoBehaviour
    {
        public static GlobalInventories Instance => instance;
        private static GlobalInventories instance;

        public IInventory PickedUp => pickedUp;

        private IInventory pickedUp;

        private void Awake()
        {
            Assert.IsNull(instance, "Multiple GlobalInventories instances");

            instance = this;

            Setup();
        }

        private void Setup()
        {
            pickedUp = new Inventory(1);
        }

}
}