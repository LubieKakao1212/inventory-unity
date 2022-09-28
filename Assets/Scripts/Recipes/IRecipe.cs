namespace Inventory.Crafting.Api
{
    using Inventory.Api;
    using Items;

    public interface IRecipe
    {
        // <summary>
        // Does the recipe ever modify input inventory during crafting
        // </summary>
        //bool ModifiesInput { get; }

        /// <summary>
        /// Returns true if this recipe can be crafted using given inventory contents without performing actual crafting <br/>
        /// Does not modify the inventory <br/>
        /// Can be used to filter currently craftable recipes
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        bool CanCraft(IInventory from);

        /// <summary>
        /// Same as <see cref="Craft(IInventory, IInventory)"/>, but <paramref name="from"/> is also used as result 
        /// </summary>
        bool Craft(IInventory from) => Craft(from, from);

        /// <summary>
        /// Performs the crafting, does all the checks and only crafts if posibble
        /// </summary>
        /// <param name="from"></param>
        /// <returns>If crafting was succesfull</returns>
        bool Craft(IInventory from, IInventory result);

        //Add ForceCraft if needed
    }
}