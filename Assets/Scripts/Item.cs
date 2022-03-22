using UnityEngine;
using System;

namespace Inventory 
{ 
    //Not a scriptable object
    //Improves runtime Item creation ability
    public class Item
    {
        public Sprite Sprite => sprite;

        public string DisplayName => displayName;

        public string Description => description;

        private Sprite sprite;
        private string displayName;
        private string description;
        //TODO receive ItemStack
        private Action itemUseAction;

        public Item(Sprite sprite, string displayName, string description = "", Action itemUseAction = null)
        {
            this.sprite = sprite;
            this.displayName = displayName;
            this.description = description;
            this.itemUseAction = itemUseAction;
        }

        //TODO receive ItemStack
        public void UseItem()
        {
            itemUseAction?.Invoke();
        }
    }
}