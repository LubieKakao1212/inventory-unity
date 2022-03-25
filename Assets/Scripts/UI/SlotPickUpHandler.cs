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
    using UnityEngine.InputSystem;

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
                switch (eventData.button)
                {
                    case PointerEventData.InputButton.Left:
                        //From picked up to this
                        if (!TryTransferAmount(GlobalInventories.Instance.PickedUp, 0, slot.Parent.Inventory, slot.SlotIndex, -1, out int transfered) || transfered == 0)
                        {
                            TrySwapSlotContents(slot.Parent.Inventory, slot.SlotIndex, GlobalInventories.Instance.PickedUp, 0);
                        }
                        break;
                    case PointerEventData.InputButton.Right:
                        //From this to picked up
                        if (!TryTransferAmount(slot.Parent.Inventory, slot.SlotIndex, GlobalInventories.Instance.PickedUp, 0, 1, out int t))
                        {
                            TrySwapSlotContents(slot.Parent.Inventory, slot.SlotIndex, GlobalInventories.Instance.PickedUp, 0);
                        }
                        break;
                }   
            }
        }
    }
}