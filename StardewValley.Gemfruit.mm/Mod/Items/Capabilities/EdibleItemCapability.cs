namespace Gemfruit.Mod.Items.Capabilities
{
    public class EdibleItemCapability : ItemCapability
    {
        public int Edibility { get; }
        public FoodType FoodType { get; }
        public FoodBuff Buff { get; }
        
        public EdibleItemCapability(int edibility, FoodType foodType, FoodBuff buff)
        {
            FoodType = foodType;
            Edibility = edibility;
            Buff = buff;
        }
    }
}