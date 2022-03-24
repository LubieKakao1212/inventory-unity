using UnityEngine;

namespace Inventory.UI
{
    using Items;

    public class Slot : MonoBehaviour
    {
        public event System.Action<ItemStack> OnContentChanged;

        public int SlotIndex => slotIndex;

        public InventoryUI Parent
        {
            get => parent;
        }

        private int slotIndex;

        private InventoryUI parent;

        public void SetItem(ItemStack newStack)
        {
            OnContentChanged?.Invoke(newStack);
        }

        public void Setup(InventoryUI parent, int slotIndex)
        {
            this.parent = parent;
            this.slotIndex = slotIndex;
        }
    }
}