using System;

namespace Gemfruit.Mod.World
{
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }
    
    public static class SeasonExxt
    {
        public static readonly string[] Names = Enum.GetNames(typeof(Season));
        public static readonly int[] Values = (int[])Enum.GetValues(typeof(Season));
        public static readonly int Count = Names.Length;
    }
}