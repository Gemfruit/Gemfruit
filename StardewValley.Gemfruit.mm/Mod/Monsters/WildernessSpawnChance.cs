using System;
using Gemfruit.Mod.Internal;
using StardewValley;

namespace Gemfruit.Mod.Monsters
{
    public delegate bool WildernessSpawnPredicate(Farm farm, Farmer player, Random rand);

    public class WildernessSpawnChance : IPrioritizable
    {
        public int Priority { get; }
        
        public RegistryKey Monster { get; }
        private WildernessSpawnPredicate _spawnPredicate;

        private static WildernessSpawnPredicate _defer = (f, p, r) => true;
        
        
        public WildernessSpawnChance(RegistryKey monster, int priority,
            WildernessSpawnPredicate spawnPredicate = null)
        {
            Monster = monster;
            Priority = priority;
            _spawnPredicate = spawnPredicate ?? _defer;
        }

        public bool Evaluate(Farm farm, Farmer player, Random rand)
        {
            return _spawnPredicate(farm, player, rand);
        }

        public override string ToString()
        {
            return Monster.ToString();
        }
    }
}