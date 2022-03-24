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
    }
}