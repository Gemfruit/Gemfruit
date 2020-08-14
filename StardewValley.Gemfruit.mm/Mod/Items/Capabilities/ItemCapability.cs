using Gemfruit.Mod.Items.Container;

namespace Gemfruit.Mod.Items.Capabilities
{
    public abstract class ItemCapability
    {
        public virtual IContainer GetContainerInstance()
        {
            return null;
        }
    }
}