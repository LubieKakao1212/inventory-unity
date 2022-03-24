using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Definition
{
    using Api;
    using Inv;

    public class InventoryHolder : MonoBehaviour
    {
        public IInventory Inventory => inventory;

        [SerializeField]
        protected int inventorySize;
        
        private IInventory inventory;

        private void Awake()
        {
            this.inventory = CreateInventory();
        }

        protected virtual IInventory CreateInventory()
        {
            return new Inventory(inventorySize);
        }
    }
}