using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class PlaceableItemCapability : ItemCapability
    {
        public ResourceKey Placeable { get; }

        public PlaceableItemCapability(ResourceKey placeable)
        {
            Placeable = placeable;
        }
    }
}