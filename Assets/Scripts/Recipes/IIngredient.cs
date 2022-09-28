namespace Inventory.Crafting.Api
{
    using Inventory.Api;
    using Items;

    public interface IIngredient
    {
        /// <summary>
        /// Are the ingredient conditions satisfied
        /// </summary>
        bool IsSatisfied { get; }

        /// <summary>
        /// Is the stack acceptable by this ingredient
        /// </summary>
        /// <returns></returns>
        bool CanAccept(ItemStack stack);

        /// <summary>
        /// Accepts the stack, returns leftover stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns>Leftover stack</returns>
        ItemStack Accept(ItemStack stack);

        //Resets internal satisfaction state
        void Reset();
    }
}
