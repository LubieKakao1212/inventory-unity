using UnityEngine;

namespace Inventory.Definition
{
    using Api;

    public class SimpleInventoryPopulator : InventoryPopulator
    {
        [SerializeField]
        private ItemStackDefinition[] insertedContent;

        public override void Populate(IInventory inv)
        { 
            base.Populate(inv);

            foreach (var stack in insertedContent)
            {
                inv.Insert(stack.ItemStack);
            }
        }
    }
}