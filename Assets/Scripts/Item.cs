using UnityEngine;
using System;

namespace Inventory.Items
{ 
    //Not a scriptable object
    //Improves runtime Item creation ability
    public class Item
    {
        public Sprite Sprite => sprite;

        public string DisplayName => displayName;

        public string Description => description;

        public int MaxStackSize => maxStackSIze;

        private readonly Sprite sprite;
        private readonly string displayName;
        private readonly string description;
        private readonly Action<ItemStack> itemUseAction;
        private readonly int maxStackSIze;

        public Item(Sprite sprite, string displayName, int maxStackSIze = 16, string description = "", Action<ItemStack> itemUseAction = null)
        {
            this.sprite = sprite;
            this.displayName = displayName;
            this.description = description;
            this.itemUseAction = itemUseAction;
            this.maxStackSIze = maxStackSIze;
        }

        /// <summary>
        /// Called from <see cref="ItemStack.Use"/>
        /// </summary>
        /// <param name="currentStack"></param>
        internal virtual void UseItem(ItemStack currentStack)
        {
            itemUseAction?.Invoke(currentStack);
        }
    }
}