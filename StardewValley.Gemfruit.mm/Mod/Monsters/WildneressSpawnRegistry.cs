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
            bool GalaxyBatSpawnPredicate(Farm f, Farmer p, Random r) =>
                r.NextDouble() < 0.01 && p.CombatLevel >= 10 && p.hasItemInInventoryNamed("Galaxy Sword");
            bool IridiumBatSpawnPredicate(Farm f, Farmer p, Random r) =>
                r.NextDouble() < 0.25 && p.CombatLevel >= 10;
            bool SerpentPredicate(Farm f, Farmer p, Random r) =>
                r.NextDouble() < 0.25 && p.CombatLevel >= 10;
            bool LavaBatPredicate(Farm f, Farmer p, Random r) =>
                r.NextDouble() < 0.5 && p.CombatLevel >= 8;
            bool FrostBatPredicate(Farm f, Farmer p, Random r) =>
                r.NextDouble() < 0.5 && p.CombatLevel >= 5;

            bool ShadowBruteSpawnPredicate(Farm f, Farmer p, Random r) =>
                r.NextDouble() < 0.15 && p.CombatLevel >= 8;
            bool WildernessGolemSpawnPredicate(Farm f, Farmer p, Random r) =>
                r.NextDouble() < 0.65;
            
            GemfruitMod.InitBus.FireEvent(new WildernessSpawnRegistrationEvent(this, EventPhase.Before, WildernessArea.Ground));
            Register(WildernessArea.Ground, 
                new WildernessSpawnChance(new RegistryKey("wild_shadow_brute"), 1, ShadowBruteSpawnPredicate));
            Register(WildernessArea.Ground, 
                new WildernessSpawnChance(new RegistryKey("wild_golem"), 2, WildernessGolemSpawnPredicate));
            Register(WildernessArea.Ground, 
                new WildernessSpawnChance(new RegistryKey("wild_slime"), 3));
            GemfruitMod.InitBus.FireEvent(new WildernessSpawnRegistrationEvent(this, EventPhase.During, WildernessArea.Ground));
            GemfruitMod.InitBus.FireEvent(new WildernessSpawnRegistrationEvent(this, EventPhase.After, WildernessArea.Ground));
            
            GemfruitMod.InitBus.FireEvent(new WildernessSpawnRegistrationEvent(this, EventPhase.Before, WildernessArea.Flying));
            Register(WildernessArea.Flying,
                new WildernessSpawnChance(new RegistryKey("galaxy_bat"), 1, GalaxyBatSpawnPredicate));
            Register(WildernessArea.Flying,
                new WildernessSpawnChance(new RegistryKey("iridium_bat"), 2, IridiumBatSpawnPredicate));
            Register(WildernessArea.Flying,
                new WildernessSpawnChance(new RegistryKey("wild_serpent"), 3, SerpentPredicate));
            Register(WildernessArea.Flying,
                new WildernessSpawnChance(new RegistryKey("lava_bat"), 4, LavaBatPredicate));
            Register(WildernessArea.Flying,
                new WildernessSpawnChance(new RegistryKey("frost_bat"), 5, FrostBatPredicate));
            Register(WildernessArea.Flying,
                new WildernessSpawnChance(new RegistryKey("wild_bat"), 6));
            GemfruitMod.InitBus.FireEvent(new WildernessSpawnRegistrationEvent(this, EventPhase.During, WildernessArea.Flying));
            GemfruitMod.InitBus.FireEvent(new WildernessSpawnRegistrationEvent(this, EventPhase.After, WildernessArea.Flying));

        }
    }
}