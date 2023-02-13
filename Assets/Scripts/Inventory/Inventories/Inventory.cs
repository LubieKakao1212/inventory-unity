using System;
using System.Linq;
using System.Collections.Generic;
using Core.Serialization;
using Newtonsoft.Json.Linq;

using UnityEngine;

namespace Inventory.Inv
{
    using Api;
    using Items;

    /// <summary>
    /// Basic <see cref="IInventory"/> implementation
    /// </summary>
    public class Inventory : IInventory, ISerializable
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

            if (stack.IsEmpty)
            {
                return ItemStack.Empty;
            }

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
            if(amount == 0)
            {
                return ItemStack.Empty;
            }
            ItemStack result = slots[slot].Copy();
            int extracted = result.Amount;
            if (amount > 0)
            {
                result.Shrink(amount);
                extracted = extracted - result.Amount;
            }
            else
            {
                result.Shrink(extracted);
            }

            if (!simulate)
            {
                if (result.IsEmpty)
                {
                    slots[slot] = ItemStack.Empty;
                }
                else
                {
                    slots[slot] = result;
                }
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

        public JToken Serialize()
        {
            JArray serialized = new JArray();
            foreach (var stack in slots)
            {
                serialized.Add(stack.Serialize());
            }

            serialized.AppendChecksum();

            return serialized;
        }

        public void Deserialize(JToken json)
        {
            if (json is JArray jarr)
            {
                jarr.ValidateChecksum();

                if (jarr.Count != Size)
                {
                    Debug.LogWarning("Target inventory size, and serialized data size do not match. Data loss possible.");
                }

                Clear(false);

                //Add items
                for (int i = 0; i < Size; i++)
                {
                    if (!Insert(ItemStack.DeserializeNew(jarr[i]), i, false).IsEmpty)
                    {
                        Debug.LogWarning("Items lost due to insertion failure");
                    }
                }

                //Notify
                for (int i = 0; i < Size; i++)
                {
                    OnContentChanged?.Invoke(i);
                }
            }
        }

        private void Clear(bool notify = false)
        {
            for (int i = 0; i < Size; i++)
            {
                slots[i] = ItemStack.Empty;
            }
            if (notify)
            {
                for (int i = 0; i < Size; i++)
                {
                    OnContentChanged?.Invoke(i);
                }
            }
        }
    }
}