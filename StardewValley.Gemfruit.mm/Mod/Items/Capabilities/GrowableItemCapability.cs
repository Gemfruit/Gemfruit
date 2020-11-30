using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class GrowableItemCapability : ItemCapability
    {
        public ResourceKey CropType { get; }

        public GrowableItemCapability(ResourceKey cropType)
        {
            CropType = cropType;
        }
    }
}