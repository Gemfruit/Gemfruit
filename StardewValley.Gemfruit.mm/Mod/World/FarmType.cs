using System;

namespace Gemfruit.Mod.World
{
    public enum FarmType
    {
        Standard = 0,
        Riverland = 1,
        Forest = 2,
        HillTop = 3,
        Wilderness = 4,
        FourCorners = 5
    }
    
    public static class FarmTypeExt
    {
        public static readonly string[] Names = Enum.GetNames(typeof(FarmType));
        public static readonly int[] Values = (int[])Enum.GetValues(typeof(FarmType));
        public static readonly int Count = Names.Length;
    }
}