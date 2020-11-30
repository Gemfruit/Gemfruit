using System.Collections.Generic;
using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Items
{
    public class ArtifactDropDataBuilder
    {
        public readonly Dictionary<ResourceKey, double> DropChances;
        
        public ArtifactDropDataBuilder()
        {
            DropChances = new Dictionary<ResourceKey, double>();
        }

        public ArtifactDropDataBuilder AddChance(ResourceKey location, double chance)
        {
            DropChances.Add(location, chance);
            return this;
        }
    }
}