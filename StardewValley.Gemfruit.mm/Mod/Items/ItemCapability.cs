namespace Gemfruit.Mod.Items
{
    public abstract class ItemCapability
    {
        public virtual IContainer GetContainerInstance()
        {
            return null;
        }
    }
}