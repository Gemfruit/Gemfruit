namespace Gemfruit.Mod.Placeables.Capabilities
{
    public class CraftingPlaceableCapability : PlaceableCapability
    {
        public bool SetOutdoors { get; }
        public bool SetIndoors { get; }
        public int Fragility { get; }
        public bool IsLamp { get; }

        public CraftingPlaceableCapability(bool outdoors, bool indoors, int fragility, bool isLamp)
        {
            SetOutdoors = outdoors;
            SetIndoors = indoors;
            Fragility = fragility;
            IsLamp = isLamp;
        }
    }
}