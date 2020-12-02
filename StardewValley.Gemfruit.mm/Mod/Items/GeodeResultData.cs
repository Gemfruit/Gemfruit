using System.Collections.Generic;
using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Items
{
    public class GeodeResultData
    {
        private List<ResourceKey> Results { get; }

        public GeodeResultData(List<ResourceKey> results)
        {
            Results = results;
        }
    }
}