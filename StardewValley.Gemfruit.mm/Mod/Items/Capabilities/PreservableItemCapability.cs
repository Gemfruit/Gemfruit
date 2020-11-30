using Gemfruit.Mod.API;
using Gemfruit.Mod.Items.Container;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class PreservableItemCapability : ItemCapability
    {
        private ResourceKey _preserveItem;

        private int _timeToReady;
        
        public PreservableItemCapability(ResourceKey preserveItem, int timeToReady)
        {
            _preserveItem = preserveItem;
            _timeToReady = timeToReady;
        }

        public virtual ResourceKey GetPreserveItem(IHasContainers item)
        {
            return _preserveItem;
        }

        public virtual int GetTimeToReady(IHasContainers item)
        {
            return _timeToReady;
        }
    }
}