namespace Inventory.Crafting.Api
{
    using Inventory.Api;
    using Items;

    public interface IProduct 
    {
        /// <summary>
        /// Applies the product to the given inventory
        /// </summary>
        /// <param name="container"></param>
        void Apply(IInventory container);
    }

}