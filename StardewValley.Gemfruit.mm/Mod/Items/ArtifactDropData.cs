using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Items
{
    public readonly struct ArtifactDropData
    {
        public readonly ResourceKey Item;
        public readonly double Percentage;
        
        public ArtifactDropData(ResourceKey item, double percentage)
        {
            Item = item;
            Percentage = percentage;
        }
    }
}