using System;

namespace Gemfruit.Mod.Items
{
    public enum FoodType
    {
        Food,
        Drink
    }

    public static class FoodTypeExt
    {
        public static readonly string[] Names = Enum.GetNames(typeof(FoodType));
        public static readonly int[] Values = (int[])Enum.GetValues(typeof(FoodType));
        public static readonly int Count = Names.Length;
    }
}