namespace Gemfruit.Mod.Items
{
    public class PreservedItemCapability : ItemCapability
    {
        public override IContainer GetContainerInstance()
        {
            return new PreservedContainer();
        }
    }
}