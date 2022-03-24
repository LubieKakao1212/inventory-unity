using UnityEngine;
using System;

namespace Inventory.Definition
{
    using Items;

    [Serializable]
    public class ItemStackDefinition
    {
        public ItemStack ItemStack => new ItemStack(item.Item, amount);
        
        [SerializeField]
        private int amount = 1;

        [SerializeField]
        private ItemDefinition item;
    }
}