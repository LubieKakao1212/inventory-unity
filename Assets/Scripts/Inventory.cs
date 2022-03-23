using System;
using System.Collections.Generic;

namespace Inventory.Inv
{
    using Api;
    using Items;

    public class Inventory : IInventory
    {
        public int Size => slots.Length;

        public ItemStack this[int slot] 
        {
            get 
            {
                ValidateSlotIndex(slot);
                return slots[slot];
            }
            set
            {
                ValidateSlotIndex(slot);
                slots[slot] = value;
            }
        }

        private ItemStack[] slots;

        public event Action<int> OnContentChanged;

        public ItemStack Insert(ItemStack stack, bool simulate = false)
        {
            throw new NotImplementedException();
        }

        public ItemStack Insert(ItemStack stack, int slot, bool simulate = false)
        {
            ValidateSlotIndex(slot);
            if (stack.CanStackWith(slots[slot]))
            {
                ItemStack existing = slots[slot].Copy();
                int notInserted = existing.Grow(stack.Amount);

                if (!simulate)
                {
                    slots[slot] = existing;
                }

                return existing.Copy(notInserted);
            }
            if (slots[slot].IsEmpty)
            {
                ItemStack result = stack.Copy();
                int notInserted = result.Amount - result.Item.MaxStackSize;

                if(notInserted > 0)
                {
                    result.Amount = result.Item.MaxStackSize;
                }

                if (!simulate)
                {
                    slots[slot] = result;
                }

                return result.Copy(notInserted);
            }
            return stack;
        }

        public ItemStack Extract(ItemStack stack, int amount = -1, bool simulate = false)
        {
            throw new NotImplementedException();
        }

        public ItemStack Extract(ItemStack stack, int slot, int amount = -1, bool simulate = false)
        {
            ValidateSlotIndex(slot);
            if (stack.CanStackWith(slots[slot]))
            {
                return Extract(slot, amount, simulate);
            }

            return ItemStack.Empty.Copy();
        }

        public ItemStack Extract(int slot, int amount = -1, bool simulate = false)
        {
            ValidateSlotIndex(slot);
            ItemStack result = slots[slot].Copy();
            int extracted = result.Amount;
            if (amount > 0)
            {
                result.Shrink(amount);
                extracted = extracted - result.Amount;
            }

            if (!simulate)
            {
                slots[slot] = result;
                OnContentChanged?.Invoke(slot);
            }

            return result.Copy(extracted);
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