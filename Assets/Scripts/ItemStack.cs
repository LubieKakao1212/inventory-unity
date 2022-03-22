using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Items
{
    public class ItemStack
    {
        public Item Item => item;
        public int Amount => amount;

        private Item item;
        private int amount;

        public ItemStack(Item Item) : this(Item, 1) { }
        
        //Lack of amount check is intended
        public ItemStack(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public void Use()
        {
            item.UseItem(this);
        }

        /// <summary>
        /// Decreses the amount of items in the stack by <paramref name="amount"/> capped at 0
        /// </summary>
        /// <returns>Amount below zero</returns>
        public int Shrink(int amount)
        {
            int result = this.amount - amount;
            if(result >= 0)
            {
                this.amount = result;
                return 0;
            }
            else
            {
                this.amount = 0;
                return -result;
            }
        }

        /// <summary>
        /// Decreses the amount of items in the stack by <paramref name="amount"/> only if resulting amont would not be negative
        /// </summary>
        /// <param name="result">Resulting amount</param>
        /// <returns>Whether anything changed</returns>
        public bool TryShrink(int amount, out int result)
        {
            result = this.amount - amount;
            if (result >= 0)
            {
                this.amount = result;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Increases the amount of items in the stack by <paramref name="amount"/> capped at item.MaxStackSize
        /// </summary>
        /// <returns>Amount above MaxStackSize</returns>
        public int Grow(int amount)
        {
            int result = this.amount + amount;
            if (result <= item.MaxStackSize)
            {
                this.amount = result;
                return 0;
            }
            else
            {
                this.amount = item.MaxStackSize;
                return result - item.MaxStackSize;
            }
        }

        /// <summary>
        /// Increases the amount of items in the stack by <paramref name="amount"/> only if resulting amont would not be greater than MaxStackSize
        /// </summary>
        /// <param name="rest">Resulting amount</param>
        /// <returns>Whether anything changed</returns>
        public bool TryGrow(int amount, out int result)
        {
            result = this.amount + amount;
            if (result <= item.MaxStackSize)
            {
                this.amount = result;
                return true;
            }
            else
            {
                result = result - item.MaxStackSize;
                return false;
            }
        }

        public ItemStack Copy()
        {
            return new ItemStack(item, amount);
        }

        public ItemStack Copy(int amount)
        {
            return new ItemStack(item, amount);
        }
    }
}