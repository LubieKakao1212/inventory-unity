using UnityEngine;
using System;

namespace Inventory.Definition
{
    using Items;

    /// <summary>
    /// Helper class for representing ItemStacks in the editor
    /// </summary>
    [Serializable]
    public class ItemStackDefinition
    {
        public ItemStack ItemStack => item != null ? new ItemStack(item.Item, amount) : ItemStack.Empty;
        
        [SerializeField]
        private int amount = 1;

        [SerializeField]
        private ItemDefinition item;
    }
}