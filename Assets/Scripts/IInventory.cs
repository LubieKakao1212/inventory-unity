using System;

namespace Inventory.Api
{
    using Items;

    public interface IInventory
    {
        event Action<int> OnContentChanged;

        int Size { get; }

        ItemStack this[int slot] { get; set; }

        /// <summary>
        /// Inserts the specified stack into the inventory.
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="simulate">Wether to actualy perform the insertion or just simulate it</param>
        /// <returns>Leftover stack</returns>
        ItemStack Insert(ItemStack stack, bool simulate = false);

        ItemStack Insert(ItemStack stack, int slot, bool simulate = false);

        ItemStack Extract(ItemStack stack, int amount = -1, bool simulate = false);

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