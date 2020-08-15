using System;

namespace Gemfruit.Mod.Items
{
    public enum WeaponType
    {
        StabbingSword = 0,
        Dagger = 1,
        Club = 2,
        SlashingSword = 3,
        Slingshot = 4
    }
    
    public static class WeaponTypeExt
    {
        public static readonly string[] Names = Enum.GetNames(typeof(WeaponType));
        public static readonly int[] Values = (int[])Enum.GetValues(typeof(WeaponType));
        public static readonly int Count = Names.Length;
    }
}