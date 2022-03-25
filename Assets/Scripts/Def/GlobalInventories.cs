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

        public IInventory PickedUp => pickedUpInventory.Inventory;

        [SerializeField]
        private InventoryHolder pickedUpInventory;

        private void Awake()
        {
            Assert.IsNull(instance, "Multiple GlobalInventories instances");

            instance = this;
        }
    }
}