using Gemfruit.Mod.API;
using Gemfruit.Mod.Items.Container;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class RoePreservableItemCapability : PreservableItemCapability
    {
        private static readonly ResourceKey CaviarKey = new ResourceKey("caviar");
        private static readonly ResourceKey SturgeonKey = new ResourceKey("sturgeon");

        private const int SturgeonTime = 6000;

        public RoePreservableItemCapability() : base(new ResourceKey("aged_roe"), 4000)
        {
            
        }

        public override ResourceKey GetPreserveItem(IHasContainers item)
        {
            if (!item.HasContainer<PreservedContainer>()) return base.GetPreserveItem(item);
            
            var preserved = item.GetContainer<PreservedContainer>().Unwrap();
            return preserved.PreservableItem.Equals(SturgeonKey) ? CaviarKey : base.GetPreserveItem(item);
        }

        public override int GetTimeToReady(IHasContainers item)
        {
            if (!item.HasContainer<PreservedContainer>()) return base.GetTimeToReady(item);
            
            var preserved = item.GetContainer<PreservedContainer>().Unwrap();
            return preserved.PreservableItem.Equals(SturgeonKey) ? SturgeonTime : base.GetTimeToReady(item);
        }
    }
}