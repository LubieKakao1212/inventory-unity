﻿using System;
using System.Collections;
using System.Collections.Generic;
using Core.Serialization;
using Newtonsoft.Json.Linq;

namespace Inventory.Items
{
    public class ItemStack : ISerializable
    {
        public static ItemStack Empty => empty.Copy();
        private static readonly ItemStack empty = new(Item.Empty, 0);

        public Item Item => item;
        public int Amount { get => amount; set => amount = value; }
        public bool IsEmpty => amount <= 0 || item == Item.Empty;

        private Item item;
        private int amount;

        public ItemStack(Item Item) : this(Item, 1) { }
        
        //Lack of amount check is intended
        public ItemStack(Item item, int amount)
        {
            this.item = item ?? Item.Empty;
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

        /// <summary>
        /// Does stacks contain the same item
        /// </summary>
        /// <returns></returns>
        public bool CanStackWith(ItemStack other)
        {
            return ReferenceEquals(item, other.item);
        }
        
        /// <summary>
        /// Creates a copy of this ItemStack
        /// </summary>
        public ItemStack Copy()
        {
            return new ItemStack(item, amount);
        }

        /// <summary>
        /// NonAlloc version of <see cref="Copy"/>
        /// </summary>
        public ItemStack CopyTo(ItemStack target) 
        {
            target.item = item;
            target.amount = amount;
            return target;
        }

        /// <summary>
        /// Creates a copy of this <see cref="ItemStack"/> with specified <paramref name="amount"/> <br/>
        /// If <paramref name="amount"/> is negative returns same as <see cref="Copy"/>
        /// </summary>
        /// <param name="amount"></param>
        public ItemStack Copy(int amount)
        {
            if(amount < 0)
            {
                amount = Amount;
            }
            return new ItemStack(item, amount);
        }

        public JToken Serialize()
        {
            var tokenOut = new JArray();

            tokenOut.Add(Item.Id);
            tokenOut.Add(Amount);

            //TODO Custom data
            //tokenOut.Add(data);
            tokenOut.Add(new JObject());

            tokenOut.AppendChecksum();

            return tokenOut;
        }

        void ISerializable.Deserialize(JToken json)
        {
            if (!(json is JArray jarr))
            {
                throw new ApplicationException("Item stacks can only be deserialized from an array");
            }
            jarr.ValidateChecksum();

            if (jarr.Count < 3)
            {
                throw new ApplicationException("Cannot deserialize ItemStack, Invalid Json data");
            }

            var id = (string)jarr[0];

            Item item = Item.Empty;

            if (id != null)
            {
                item = ItemDB.Instance[id];
            }

            var amount = (int?)jarr[1];

            this.item = amount.HasValue ? item : Item.Empty;
            this.amount = amount.GetValueOrDefault(0);

            //TODO
            //data = jarr[2]
        }

        public static ItemStack DeserializeNew(JToken json)
        {
            var stack = (ISerializable)Empty;
            stack.Deserialize(json);
            return (ItemStack)stack;
        }
    }
}