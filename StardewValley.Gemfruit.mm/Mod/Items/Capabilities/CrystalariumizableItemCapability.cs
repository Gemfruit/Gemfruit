namespace Gemfruit.Mod.Items.Capabilities
{
    public class CrystalariumizableItemCapability : ItemCapability
    {
        public const int DefaultCrystalTime = 5000;
        
        public int Minutes { get; }

        public CrystalariumizableItemCapability(int minutes)
        {
            Minutes = minutes;
        }
    }
}