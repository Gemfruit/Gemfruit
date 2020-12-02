using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Items.Crafting.Recipe
{
    public class CrystalariumRecipe
    {
        public ResourceKey Material { get; }
        public int TimeToReady { get; }
        
        public CrystalariumRecipe(ResourceKey material, int timeToReady)
        {
            Material = material;
            TimeToReady = timeToReady;
        }
    }
}