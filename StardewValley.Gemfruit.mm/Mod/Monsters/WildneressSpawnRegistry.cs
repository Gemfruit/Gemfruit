using System;
using System.Collections.Generic;
using System.Linq;
using Gemfruit.Mod.Events.Monsters;
using Gemfruit.Mod.Internal;
using StardewValley;
using StardewValley.Locations;

namespace Gemfruit.Mod.Monsters
{
    public class WildernessSpawnRegistry : SortableListRegistry<WildernessArea, WildernessSpawnChance>
    {
        public Optional<RegistryKey> Get(WildernessArea area, Farm farm, Farmer player, Random rand)
        {
            if (CurrentPhase == RegistryPhase.Frozen)
            {
                foreach (var p in _dictionary[area].Where(p => p.Evaluate(farm, player, rand)))
                {
                    return new Optional<RegistryKey>(p.Monster);
                }
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.ERROR, "WildernessSpawnRegistry",
                    $"Attempted to get spawn for area '{area}' before registration was done!");
            }

            return Optional<RegistryKey>.None();
        }

        protected override void InitializeRecords()
        {

        }
    }
}