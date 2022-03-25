using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Inventory.Inv
{
    using Api;
    using Items;

    public static class InventoryUtil
    {
        /// <summary>
        /// Attempts to swap contents of two slots from two inventories
        /// </summary>
        /// <returns>Whether swap was succesful</returns>
        public static bool TrySwapSlotContents(IInventory inv1, int slot1, IInventory inv2, int slot2)
        {
            ItemStack existing1 = inv1[slot1];
            ItemStack existing2 = inv2[slot2];
            
            ItemStack stack1 = inv1.Extract(slot1, -1);
            ItemStack stack2 = inv2.Extract(slot2, -1);

            ItemStack leftover1 = inv2.Insert(stack1, slot2);
            ItemStack leftover2 = inv1.Insert(stack2, slot1);

            if(!leftover1.IsEmpty || !leftover2.IsEmpty)
            {
                inv1[slot1] = existing1;
                inv2[slot2] = existing2;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Attempts to transfer given amount of items from one slot two another
        /// </summary>
        /// <param name="amountTransfered">How many items were actualy transfered</param>
        /// <returns>Whether transfer was succesful</returns>
        public static bool TryTransferAmount(IInventory from, int fromSlot, IInventory to, int toSlot, int amount, out int amountTransfered)
        {
            ItemStack existingFrom = from[fromSlot];
            ItemStack existingTo = to[toSlot];

            if (amount <= 0)
            {
                amount = existingFrom.Amount;
            }

            ItemStack extracted = from.Extract(fromSlot, amount, true);

            ItemStack leftover = to.Insert(extracted, toSlot);

            amountTransfered = amount - leftover.Amount;

            leftover = from.Extract(fromSlot, amountTransfered);

            if (leftover.Amount != amountTransfered)
            {
                amountTransfered = 0;
                from[fromSlot] = existingFrom;
                to[toSlot] = existingTo;
                return false;
            }

            return true;
        }
    }
}
