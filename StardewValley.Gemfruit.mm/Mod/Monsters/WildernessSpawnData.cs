using System;
using Microsoft.Xna.Framework;
using StardewValley;

namespace Gemfruit.Mod.Monsters
{
    public readonly struct WildernessSpawnData
    {
        public readonly Farm farm;
        public readonly Farmer player;
        public readonly Random rand;
        public readonly Vector2 position;
    }
}