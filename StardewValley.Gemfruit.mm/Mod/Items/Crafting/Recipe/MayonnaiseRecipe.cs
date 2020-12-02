using Gemfruit.Mod.API;
using Gemfruit.Mod.Items.Container;

namespace Gemfruit.Mod.Items.Crafting.Recipe
{
    public class MayonnaiseRecipe
    {
        public ResourceKey Material { get; }
        public ResourceKey Result { get; }
        public int TimeToReady { get; }
        public ItemQuality Quality { get; }

        public MayonnaiseRecipe(ResourceKey material, ResourceKey result, int timeToReady,
            ItemQuality quality = ItemQuality.None)
        {
            Material = material;
            Result = result;
            TimeToReady = timeToReady;
            Quality = quality;
        }

        public virtual bool Matches(IHasKey c)
        {
            return c.GetResourceKey().Equals(Material);
        }

        public virtual ResourceKey GetResult(IHasContainers item)
        {
            return Result;
        }

        public virtual int GetTimeToReady(IHasContainers item)
        {
            return TimeToReady;
        }
    }
}