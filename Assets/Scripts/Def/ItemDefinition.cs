using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Definition
{
    using Items;

    /// <summary>
    /// Definition of an Item and its instance in a form of a scriptable object
    /// </summary>
    [CreateAssetMenu(menuName = "Item")]
    public class ItemDefinition : ScriptableObject
    {
        public Item Item
        {
            get
            {
                if (itemInstance == null)
                {
                    itemInstance = CreateItem();
                }

                return itemInstance;
            }
        }

        [SerializeField]
        private Sprite sprite = null;

        [SerializeField]
        private string displayName = "";

        [SerializeField]
        private int maxStackSize = 16;

        private Item itemInstance;

        /// <summary>
        /// Creates a new Item instance from this definition
        /// </summary>
        /// <returns></returns>
        public virtual Item CreateItem()
        {
            return new Item(sprite, displayName, maxStackSize);
        }
    }
}