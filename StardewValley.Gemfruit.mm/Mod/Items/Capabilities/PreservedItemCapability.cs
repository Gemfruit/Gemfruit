using Gemfruit.Mod.Items.Container;

namespace Gemfruit.Mod.Items.Capabilities
{
    public class PreservedItemCapability : ItemCapability
    {
        public override IContainer GetContainerInstance()
        {
            return new PreservedContainer();
        }
    }
}