using Gemfruit.Mod.Items;

namespace Gemfruit.Mod.Placeables.Capabilities
{
    public class FurniturePlaceableCapability : PlaceableCapability
    {
        public FurnitureType FurnitureType { get; }

        public FurniturePlaceableCapability(FurnitureType furnitureType)
        {
            FurnitureType = furnitureType;
        }
    }
}