using System.Collections.Generic;

namespace Gemfruit.Mod.Items
{
    public class GeodeItem : Item
    {
        public List<int> Results { get; private set; }

        protected internal GeodeItem(Item bas, List<int> results) : base(bas)
        {
            Results = results;
        }
    }
}