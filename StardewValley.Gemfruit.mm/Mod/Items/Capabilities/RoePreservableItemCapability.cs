using Gemfruit.Mod.API;
using Gemfruit.Mod.Items.Container;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class RoePreservableItemCapability : PreservableItemCapability
    {
        private static readonly ResourceKey CAVIAR_KEY = new ResourceKey("caviar");
        private static readonly ResourceKey STURGEON_KEY = new ResourceKey("sturgeon");

        private static readonly int STURGEON_TIME = 6000;
        
        public RoePreservableItemCapability() : base(new ResourceKey("aged_roe"), 4000)
        {
            
        }

        public override ResourceKey GetPreserveItem(IHasContainers item)
        {
            if (!item.HasContainer(typeof(PreservedContainer))) return base.GetPreserveItem(item);
            
            var preserved = item.GetContainer<PreservedContainer>();
            return preserved.PreservableItem.Equals(STURGEON_KEY) ? CAVIAR_KEY : base.GetPreserveItem(item);
        }

        public override int GetTimeToReady(IHasContainers item)
        {
            if (!item.HasContainer(typeof(PreservedContainer))) return base.GetTimeToReady(item);
            
            var preserved = item.GetContainer<PreservedContainer>();
            return preserved.PreservableItem.Equals(STURGEON_KEY) ? STURGEON_TIME : base.GetTimeToReady(item);
        }
    }
}