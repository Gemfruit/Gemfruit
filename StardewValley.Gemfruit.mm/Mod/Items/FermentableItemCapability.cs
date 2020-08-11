using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Items
{
    public class FermentableItemCapability : ItemCapability
    {
        public RegistryKey FermentedItem { get; }

        public FermentableItemCapability(RegistryKey fermentedItem)
        {
            FermentedItem = fermentedItem;
        }
    }
}