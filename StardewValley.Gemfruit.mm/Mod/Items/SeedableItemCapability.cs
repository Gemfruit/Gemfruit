using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Items
{
    public class SeedableItemCapability : ItemCapability
    {
        public RegistryKey SeedItem { get; }

        public SeedableItemCapability(RegistryKey seedItem)
        {
            SeedItem = seedItem;
        }
    }
}