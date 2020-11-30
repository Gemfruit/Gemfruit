using System;
using Microsoft.Xna.Framework;
using StardewValley;

namespace Gemfruit.Mod.Monsters
{
    public struct WildernessSpawnData
    {
        public readonly Farm Farm;
        public readonly Farmer Player;
        public readonly Random Rand;
        public readonly Vector2 Position;
    }
}