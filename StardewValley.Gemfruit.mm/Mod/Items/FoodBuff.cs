using System;
using System.Collections.Generic;

namespace Gemfruit.Mod.Items
{
    public struct FoodBuff
    {
        public int Farming { get; private set; }
        public int Fishing { get; private set; }
        public int Mining { get; private set; }
        public int Digging { get; private set; }
        public int Luck { get; private set; }
        public int Foraging { get; private set; }
        public int Crafting { get; private set; }
        public int MaxEnergy { get; private set; }
        public int Magnetism { get; private set; }
        public int Speed { get; private set; }
        public int Defense { get; private set; }
        public int Attack { get; private set; }
        
        public int Duration { get; private set; }

        public FoodBuff(IReadOnlyList<int> buffs, int duration)
        {
            if(buffs.Count < 10) throw new ArgumentException("FoodBuff buffs array must be >=10");
            Farming = buffs[0];
            Fishing = buffs[1];
            Mining = buffs[2];
            Digging = buffs[3];
            Luck = buffs[4];
            Foraging = buffs[5];
            Crafting = buffs[6];
            MaxEnergy = buffs[7];
            Magnetism = buffs[8];
            Speed = buffs[9];
            Defense = buffs.Count >= 11 ? buffs[10] : 0;
            Attack = buffs.Count > 12 ? buffs[11] : 0;

            Duration = duration;
        }
    }
}