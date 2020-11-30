using Gemfruit.Mod.API;
using Gemfruit.Mod.Items.Container;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class SmeltableItemCapability
    {
        public ResourceKey _smeltedItem;
        public int _timeToReady;

        public SmeltableItemCapability(ResourceKey smeltedItem, int timeToReady)
        {
            _smeltedItem = smeltedItem;
            _timeToReady = timeToReady;
        }

        public virtual ResourceKey GetSmeltedItem(IHasContainers item)
        {
            return _smeltedItem;
        }

        public virtual int GetTimeToReady(IHasContainers item)
        {
            return _timeToReady;
        }
    }
}