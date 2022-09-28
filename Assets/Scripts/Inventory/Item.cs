using UnityEngine;
using System;

namespace Inventory.Items
{ 
    //Not a scriptable object
    //Improves runtime Item creation ability
    public class Item
    {
        public static Item Empty => empty;
        private static readonly Item empty = new Item(null, null, null, 0, null, null);

        public Sprite Sprite => sprite;

        public string DisplayName => displayName;

        public string Description => description;

        public int MaxStackSize => maxStackSIze;

        public string Id => id;

        private readonly Sprite sprite;
        private readonly string displayName;
        private readonly string description;
        private readonly Action<ItemStack> itemUseAction;
        private readonly int maxStackSIze;
        private readonly string id;
        
        /// <param name="Id">Id can be null for non serializable items, but in most cases you don't want it to be null</param>
        public Item(string id, Sprite sprite, string displayName, int maxStackSIze = 16, string description = "", Action<ItemStack> itemUseAction = null)
        {
            this.id = id;
            this.sprite = sprite;
            this.displayName = displayName;
            this.description = description;
            this.itemUseAction = itemUseAction;
            this.maxStackSIze = maxStackSIze;

            if (this.id != null)
            {
                ItemDB.Instance.RegisterItem(this);
            }
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