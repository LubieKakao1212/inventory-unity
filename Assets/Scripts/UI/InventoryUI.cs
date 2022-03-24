using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    using Api;

    public class InventoryUI : MonoBehaviour
    {
        public IInventory Inventory
        {
            set
            {
                if (inventory != null)
                {
                    DestroyVisuals();
                    inventory.OnContentChanged -= ReceiveContentChange;
                }
                inventory = value;
                if (inventory != null)
                {
                    CreateVisuals();
                    inventory.OnContentChanged += ReceiveContentChange;
                }
            }
        }

        [SerializeField]
        private Slot slotPrefab;

        private IInventory inventory;

        private List<Slot> slots;

        private void Awake()
        {
            slots = new List<Slot>();
        }

        private void DestroyVisuals() 
        {
            foreach (var slot in slots)
            {
                Destroy(slot);
            }
            slots.Clear();
        }

        private void CreateVisuals()
        {
            for (int i = 0; i < inventory.Size; i++) 
            {
                slots.Add(Instantiate(slotPrefab, transform));
                slots[i].SetItem(inventory[i]);
            }
        }

        private void ReceiveContentChange(int slot)
        {
            slots[slot].SetItem(inventory[slot]);
        }
    }
}