using Gemfruit.Mod.API;
using Gemfruit.Mod.Items.Container;

namespace Gemfruit.Mod.Items.Crafting.Recipe
{
    public class RoePreservesRecipe : PreservesRecipe
    {
        private static readonly ResourceKey SturgeonKey = new ResourceKey("sturgeon");
        private static readonly ResourceKey CaviarKey = new ResourceKey("caviar");
        private const int CaviarTime = 6000;

        public RoePreservesRecipe(ResourceKey material, ResourceKey result, int timeToReady) : base(material, result, timeToReady)
        {
            
        }

        private static bool DoesProduceCaviar(IHasContainers item)
        {
            return item.HasContainer<BaseItemContainer>() &&
                   Equals(item.GetContainer<BaseItemContainer>().Unwrap().BaseItem, SturgeonKey);
        }

        public override ResourceKey GetResult(IHasContainers item)
        {
            return DoesProduceCaviar(item) ? CaviarKey : base.GetResult(item);
        }

        public override int GetTimeToReady(IHasContainers item)
        {
            return DoesProduceCaviar(item) ? CaviarTime : base.GetTimeToReady(item);
        }

        public override void FillBaseItem(IHasContainers item, IHasContainers material)
        {
            if (!DoesProduceCaviar(item) && item.HasContainer<BaseItemContainer>() && material.HasContainer<BaseItemContainer>())
            {
                item.GetContainer<BaseItemContainer>().Unwrap().BaseItem =
                    material.GetContainer<BaseItemContainer>().Unwrap().BaseItem;
            }
        }
    }
}