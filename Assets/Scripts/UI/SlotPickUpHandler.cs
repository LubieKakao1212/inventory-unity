using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    using Definition;
    using Inv;
    using Api;
    using static Inv.InventoryUtil;

    [RequireComponent(typeof(Slot))]
    public class SlotPickUpHandler : MonoBehaviour, IPointerDownHandler
    {
        private Slot slot;

        private void Start()
        {
            slot = GetComponent<Slot>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (enabled)
            {
                TrySwapSlotContents(slot.Parent.Inventory, slot.SlotIndex, GlobalInventories.Instance.PickedUp, 0);
            }
        }
    }
}