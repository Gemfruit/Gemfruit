using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class FermentableItemCapability : ItemCapability
    {
        public ResourceKey FermentedItem { get; }

        public FermentableItemCapability(ResourceKey fermentedItem)
        {
            FermentedItem = fermentedItem;
        }
    }
}