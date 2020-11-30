using System.Collections.Generic;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class CrackableItemCapability : ItemCapability
    {
        public List<int> Results { get; }

        public CrackableItemCapability(List<int> res)
        {
            Results = res;
        } 
    }
}