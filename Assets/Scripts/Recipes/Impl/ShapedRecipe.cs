using System.Collections.Generic;

namespace Inventory.Crafting.Impl
{
    using Api;
    using Inventory.Api;
    using Items;

    public class ShapedRecipe : IRecipe
    {
        private Dictionary<int, IIngredient> ingredients;
        private List<IProduct> products;

        private ItemStack workStack = new ItemStack(Item.Empty);
        
        public bool CanCraft(IInventory from)
        {
            foreach (var ingr in ingredients)
            {
                if (ingr.Key >= from.Size)
                {
                    return false;
                }

                var stack = from[ingr.Key].CopyTo(workStack);
                
                if (!ingr.Value.CanAccept(stack))
                {
                    return false;
                }

                ingr.Value.Accept(stack);

                if (!ingr.Value.IsSatisfied)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Craft(IInventory from, IInventory result)
        {
            
        }
    }
}