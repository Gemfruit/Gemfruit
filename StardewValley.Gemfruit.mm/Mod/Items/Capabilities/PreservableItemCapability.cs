using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class PreservableItemCapability : ItemCapability
    {
        public ResourceKey PreserveItem { get; }

        public PreservableItemCapability(ResourceKey preserveItem)
        {
            PreserveItem = preserveItem;
        }
    }
}