using UnityEngine;

namespace Inventory.UI
{
    using Definition;

    public class InventoryAttacher : MonoBehaviour
    {
        [SerializeField]
        private InventoryHolder inventory;

        [SerializeField]
        private InventoryUI invUI;

        private void Start()
        {
            invUI.Inventory = inventory.Inventory;
        }
    }
}