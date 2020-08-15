using System;
using Microsoft.Xna.Framework;
using StardewValley.Locations;

namespace Gemfruit.Mod.Monsters
{
    public readonly struct MineshaftSpawnData
    {
        public readonly MineShaft Shaft;
        public readonly Random Rand;
        public readonly Vector2 Position;
        public readonly int Area;
        public readonly int Level;

        public MineshaftSpawnData(MineShaft shaft, Random rand, Vector2 position, int area, int level)
        {
            Shaft = shaft;
            Rand = rand;
            Position = position;
            Area = area;
            Level = level;
        }
    }
    
}