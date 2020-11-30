using Gemfruit.Mod.API;
using Gemfruit.Mod.Items.Container;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class MayonnaiseableItemCapability : ItemCapability
    {
        private ResourceKey _mayonaiseItem;
        private int _timeToReady;
        private ItemQuality _quality;

        public MayonnaiseableItemCapability(ResourceKey mayonaiseItem, int timeToReady = 180, ItemQuality quality = ItemQuality.None)
        {
            _mayonaiseItem = mayonaiseItem;
            _timeToReady = timeToReady;
            _quality = quality;
        }

        public virtual ResourceKey GetMayonaiseItem(IHasContainers item)
        {
            return _mayonaiseItem;
        }

        public virtual int GetTimeToReady(IHasContainers item)
        {
            return _timeToReady;
        }

        public virtual ItemQuality GetQuality(IHasContainers item)
        {
            return _quality;
        }
    }
}