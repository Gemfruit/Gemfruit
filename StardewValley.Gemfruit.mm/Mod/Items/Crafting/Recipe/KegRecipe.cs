using Gemfruit.Mod.API;
using Gemfruit.Mod.Items.Container;

namespace Gemfruit.Mod.Items.Crafting.Recipe
{
    public class KegRecipe
    {
        public ResourceKey Material { get; }
        public ResourceKey Result { get; }
        public int TimeToReady { get; }
        public int StackSize { get; }

        public KegRecipe(ResourceKey material, ResourceKey result, int timeToReady, int stackSize = 1)
        {
            Material = material;
            Result = result;
            TimeToReady = timeToReady;
            StackSize = stackSize;
        }

        public virtual bool Matches(IStackInfo c)
        {
            return c.GetResourceKey().Equals(Material) && c.GetQuantity() > StackSize;
        }
        
        public virtual ResourceKey GetResult(IHasContainers item)
        {
            return Result;
        }

        public virtual int GetTimeToReady(IHasContainers item)
        {
            return TimeToReady;
        }

        /// <summary>
        /// Fills the provided <see cref="item"/> with a base material.
        /// </summary>
        /// <remarks>
        /// By default, the base item is the Material key of the recipe, but the functions is also given
        /// the original <see cref="material"/> item with container access, such that the information can
        /// be copied/derived from instance data on the original item.
        /// </remarks>
        /// <param name="item">Item to fill with base item data</param>
        /// <param name="material">Original item to derive information from (unused by default)</param>
        public virtual void FillBaseItem(IHasContainers item, IHasContainers material)
        {
            if(item.HasContainer<BaseItemContainer>())
            {
                item.GetContainer<BaseItemContainer>().Unwrap().BaseItem = Material;
            }
        }
    }
}