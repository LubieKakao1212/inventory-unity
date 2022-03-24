using System;

namespace Inventory.Api
{
    using Items;

    public interface IInventory
    {
        event Action<int> OnContentChanged;
        
        /// <summary>
        /// Size of the inventory
        /// </summary>
        int Size { get; }
        
        /// <summary>
        /// Acceses specific slot in the Inventory
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        ItemStack this[int slot] { get; set; }

        /// <summary>
        /// Inserts the specified stack into the inventory
        /// </summary>
        /// <param name="simulate">Wether to actualy perform the insertion or just simulate it</param>
        /// <returns>Leftover stack</returns>
        ItemStack Insert(ItemStack stack, bool simulate = false);

        /// <summary>
        /// Inserts the specified stack into the specified slot of the inventory
        /// </summary>
        /// <param name="slot">The slot to insert to</param>
        /// <param name="simulate">Wether to actualy perform the insertion or just simulate it</param>
        /// <returns></returns>
        ItemStack Insert(ItemStack stack, int slot, bool simulate = false);

        /// <summary>
        /// Extracts specified amount of items from the inventory. Only extracts items that can stack with provided <paramref name="stack"/>
        /// </summary>
        /// <param name="amount">Maximum amount to be extracted</param>
        /// <param name="simulate">Wether to actualy perform the extraction or just simulate it</param>
        /// <returns>Extracted stack</returns>
        ItemStack Extract(ItemStack stack, int amount = -1, bool simulate = false);

        /// <summary>
        /// Extracts specified amount of items from specified slot in the inventory. Only extracts items that can stack with provided <paramref name="stack"/>
        /// </summary>
        /// <param name="amount">Maximum amount to be extracted</param>
        /// <param name="simulate">Wether to actualy perform the extraction or just simulate it</param>
        /// <returns>Extracted stack</returns>
        ItemStack Extract(ItemStack stack, int slot, int amount = -1, bool simulate = false);

        /// <summary>
        /// Extracts stack from specified slot.
        /// </summary>
        /// <param name="amount">Maximum amount to be extracted</param>
        /// <param name="simulate">Wether to actualy perform the extraction or just simulate it</param>
        /// <returns>Extracted stack</returns>
        ItemStack Extract(int slot, int amount = -1, bool simulate = false);
    }
}