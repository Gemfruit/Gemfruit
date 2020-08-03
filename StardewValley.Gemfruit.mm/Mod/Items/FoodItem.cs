namespace Gemfruit.Mod.Items
{
    public class FoodItem : Item
    {
        public FoodType FoodType { get; private set; }
        public FoodBuff Buff { get; private set; }

        protected internal FoodItem(Item bas, FoodType foodType, FoodBuff buff) : base(bas)
        {
            FoodType = foodType;
            Buff = buff;
        }
        
    }
}