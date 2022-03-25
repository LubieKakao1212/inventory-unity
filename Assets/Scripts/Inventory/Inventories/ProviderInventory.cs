using System.Linq;

namespace Inventory.Inv
{
    using Api;
    using Items;
    using System;

    /// <summary>
    /// <see cref="IInventory"/> implementation which always provieds the same content to <see cref="Extract(int, int, bool)"/>
    /// </summary>
    public class ProviderInventory : IInventory
    {
        public ItemStack this[int slot]
        {
            get
            {
                ValidateSlotIndex(slot);
                return templates[slot];
            }
            set
            {
                ValidateSlotIndex(slot);
                templates[slot] = value;
                OnContentChanged?.Invoke(slot);
            }
        }

        public int Size => templates.Length;

        public event Action<int> OnContentChanged;
        
        private ItemStack[] templates;
        
        public ProviderInventory(int size)
        {
            templates = Enumerable.Repeat(ItemStack.Empty, size).ToArray();
        }

        public ItemStack Extract(ItemStack stack, int amount = -1, bool simulate = false)
        {
            return Extract(stack, 0, amount, simulate);
        }

        public ItemStack Extract(ItemStack stack, int slot, int amount = -1, bool simulate = false)
        {
            ValidateSlotIndex(slot);
            if (stack.CanStackWith(templates[slot]))
            {
                return Extract(slot, amount, simulate);
            }

            return ItemStack.Empty;
        }

        public ItemStack Extract(int slot, int amount = -1, bool simulate = false)
        {
            ValidateSlotIndex(slot);
            if (amount == 0)
            {
                amount = -1;
            }
            return templates[slot].Copy(amount);
        }

        public ItemStack Insert(ItemStack stack, bool simulate = false)
        {
            return Insert(stack, 0, simulate);
        }

        public ItemStack Insert(ItemStack stack, int slot, bool simulate = false)
        {
            return ItemStack.Empty;
        }

        private void ValidateSlotIndex(int i)
        {
            if (i < 0 || i >= Size)
            {
                throw new IndexOutOfRangeException($"Slot index is outside of bounds: {i}, Size: {Size}");
            }
        }
    }
}