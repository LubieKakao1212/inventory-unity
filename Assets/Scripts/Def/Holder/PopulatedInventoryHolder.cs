using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Definition
{
    using Api;
    using Inv;
    using Items;
    
    public class PopulatedInventoryHolder : InventoryHolder
    {
        [SerializeField]
        private InventoryPopulator populator;

        protected override IInventory CreateInventory()
        {
            IInventory inv = base.CreateInventory();
            populator.Populate(inv);
            return inv;
        }
    }
}