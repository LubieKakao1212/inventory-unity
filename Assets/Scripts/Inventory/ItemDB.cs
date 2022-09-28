using System;
using System.Collections.Generic;

namespace Inventory.Items
{
    public class ItemDB
    {
        public static ItemDB Instance { get; } = new ItemDB();

        public Item this[string id] 
        {
            get
            {
                if (registeredItems.TryGetValue(id, out var item))
                {
                    return item;
                }
                return null;
            }
        }

        private Dictionary<string, Item> registeredItems = new();

        public void RegisterItem(Item item)
        {
            if (registeredItems.ContainsKey(item.Id))
            {
                throw new ArgumentException($"Item \"{item.Id}\" already registered");
            }
            registeredItems.Add(item.Id, item);
        }
    }
}
