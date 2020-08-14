using System.Collections.Generic;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class GeodizableItemCapability : ItemCapability
    {
        public List<int> Results { get; }

        public GeodizableItemCapability(List<int> res)
        {
            Results = res;
        } 
    }
}