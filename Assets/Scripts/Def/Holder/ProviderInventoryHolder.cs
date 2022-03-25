using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Definition
{
    using Api;
    using Inv;
    using Items;

    public class ProviderInventoryHolder : InventoryHolder
    {
        [SerializeField]
        private ItemStackDefinition[] content;

        protected override IInventory CreateInventory()
        {
            IInventory inv = new ProviderInventory(content.Length);
            for (int i = 0; i < content.Length; i++) 
            {
                inv[i] = content[i].ItemStack;
            }
            return inv;
        }

    }
}