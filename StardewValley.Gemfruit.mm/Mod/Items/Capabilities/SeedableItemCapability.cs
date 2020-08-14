using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class SeedableItemCapability : ItemCapability
    {
        public ResourceKey SeedItem { get; }

        public SeedableItemCapability(ResourceKey seedItem)
        {
            SeedItem = seedItem;
        }
    }
}