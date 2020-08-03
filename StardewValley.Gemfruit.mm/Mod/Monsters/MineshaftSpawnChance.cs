using System;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Utility;
using Gemfruit.Mod.Internal;
using StardewValley.Locations;

namespace Gemfruit.Mod.Monsters
{
    public delegate bool MineLevelPredicate(MineShaft shaft, Random r, int xTile, int yTile);

    public class MineshaftSpawnChance : IPrioritizable
    {
        public int Priority { get; }

        public RegistryKey Monster { get; }
        public int FirstLevel { get; }
        public int LastLevel { get; }
        private MineLevelPredicate _spawnPredicate;

        private static MineLevelPredicate _defer = (s, r, x, y) => true;

        public MineshaftSpawnChance(RegistryKey monster, int firstLevel, int lastLevel, int priority,
            MineLevelPredicate spawnPredicate = null)
        {
            Monster = monster;
            FirstLevel = firstLevel;
            LastLevel = lastLevel;
            Priority = priority;
            _spawnPredicate = spawnPredicate ?? _defer;
        }

        public bool Evaluate(MineShaft shaft, Random rand, int level, int xTile, int yTile)
        {
            if (FirstLevel == 0 && LastLevel == 0) 
                return _spawnPredicate(shaft, rand, xTile, yTile);
            
            if (level < FirstLevel || level > LastLevel) 
                return false;
            
            return _spawnPredicate(shaft, rand, xTile, yTile);
        }

        public override string ToString()
        {
            return Monster.ToString();
        }
    }
}