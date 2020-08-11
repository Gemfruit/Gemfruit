using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Items
{
    public class PreservableItemCapability : ItemCapability
    {
        public RegistryKey PreserveItem { get; }

        public PreservableItemCapability(RegistryKey preserveItem)
        {
            PreserveItem = preserveItem;
        }
    }
}