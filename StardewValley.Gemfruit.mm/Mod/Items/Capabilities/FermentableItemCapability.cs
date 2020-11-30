using Gemfruit.Mod.API;
using Gemfruit.Mod.Items.Container;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class FermentableItemCapability : ItemCapability
    {
        private ResourceKey _fermentedItem;
        private int _timeToReady;
        private int _stackSize;

        public FermentableItemCapability(ResourceKey fermentedItem, int timeToReady, int stackSize = 1)
        {
            _fermentedItem = fermentedItem;
            _timeToReady = timeToReady;
            _stackSize = stackSize;
        }

        public virtual ResourceKey GetFermentedItem(IHasContainers item)
        {
            return _fermentedItem;
        }

        public virtual int GetTimeToReady(IHasContainers item)
        {
            return _timeToReady;
        }

        public virtual int GetStackSize(IHasContainers item)
        {
            return _stackSize;
        }
    }
}