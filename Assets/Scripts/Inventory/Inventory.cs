using System;
using System.Linq;
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
                OnContentChanged?.Invoke(slot);
            }
        }

        private ItemStack[] slots;

        public event Action<int> OnContentChanged;

        public Inventory(int size)
        {
            slots = Enumerable.Repeat(ItemStack.Empty, size).ToArray();
        }

        public ItemStack Insert(ItemStack stack, bool simulate = false)
        {
            //No need to copy stack it is copied inside Insert(ItemStack, int, bool)
            for (int i = 0; i < Size; i++) 
            {
                stack = Insert(stack, i, simulate);
            }

            return stack;
        }

        public ItemStack Insert(ItemStack stack, int slot, bool simulate = false)
        {
            ValidateSlotIndex(slot);

            ItemStack inserted = null;
            ItemStack notInserted = null;
            
            if (stack.CanStackWith(slots[slot]))
            {
                inserted = slots[slot].Copy();
                int notInsertedAmount = inserted.Grow(stack.Amount);
                
                notInserted = inserted.Copy(notInsertedAmount);
            }
            if (slots[slot].IsEmpty)
            {
                inserted = stack.Copy();
                int notInsertedAmount = inserted.Amount - inserted.Item.MaxStackSize;

                if(notInsertedAmount > 0)
                {
                    inserted.Amount = inserted.Item.MaxStackSize;
                }
                else
                {
                    notInsertedAmount = 0;
                }

                notInserted = inserted.Copy(notInsertedAmount);
            }

            if (inserted == null)
            {
                return stack;
            }

            if (!simulate)
            {
                slots[slot] = inserted;
                OnContentChanged?.Invoke(slot);
            }

            return notInserted;
        }

        public ItemStack Extract(ItemStack stack, int amount = -1, bool simulate = false)
        {
            ItemStack result = stack.Copy(0);

            if(amount <= 0)
            {
                amount = result.Item.MaxStackSize;
            }

            for (int i = 0; i < Size; i++) 
            {
                int amt = Extract(stack, amount, simulate).Amount;

                result.Grow(amt);
                amount -= amt;

                if(amount == 0) 
                {
                    break;
                }

                if (amount < 0)
                {
                    throw new Exception("This should never happen");
                }
            }

            return result;
        }

        public ItemStack Extract(ItemStack stack, int slot, int amount = -1, bool simulate = false)
        {
            ValidateSlotIndex(slot);
            if (stack.CanStackWith(slots[slot]))
            {
                return Extract(slot, amount, simulate);
            }

            return ItemStack.Empty;
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