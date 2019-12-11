using System;
using System.Linq;
using Gemfruit.Mod.Events.Monsters;
using Gemfruit.Mod.Internal;
using StardewValley.Locations;

namespace Gemfruit.Mod.Monsters
{
    public class MineshaftSpawnRegistry : SortableListRegistry<MineshaftArea, MineshaftSpawnChance>
    {
        public Optional<RegistryKey> Get(MineshaftArea area, MineShaft shaft, Random rand, int level, int xTile, int yTile)
        {
            if (CurrentPhase == RegistryPhase.Frozen)
            {
                foreach (var p in _dictionary[area].Where(p => p.Evaluate(shaft, rand, level, xTile, yTile)))
                {
                    return new Optional<RegistryKey>(p.Monster);
                }
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.ERROR, "MineshaftSpawnRegistry",
                    $"Attempted to get spawn for area '{area}' before registration was done!");
            }

            return Optional<RegistryKey>.None();
        }

        protected override void InitializeRecords()
        {
            bool InfestedBigSlimePredicate(MineShaft s, Random r, int x, int y) => r.NextDouble() < 0.2;
            bool DinoAreaPredicate(MineShaft s, Random r, int x, int y) => r.NextDouble() < 0.1;
            bool RandomBugPredicate(MineShaft s, Random r, int x, int y) =>
                r.NextDouble() < 0.25 && s.mustKillAllMonstersToAdvance();
            bool DuggyPredicate(MineShaft s, Random r, int x, int y) => s.doesTileHaveProperty(x, y, "Diggable", "Back") != null;
            bool RockCrabPredicate(MineShaft s, Random r, int x, int y) => r.NextDouble() < 0.15;
            bool FlyPredicate(MineShaft s, Random r, int x, int y) =>
                r.NextDouble() < 0.05 && s.getDistanceFromStart(x, y) > 10.0;
            bool GreenSlimePredicate(MineShaft s, Random r, int x, int y) => r.NextDouble() < 0.45;
            bool BatPredicate(MineShaft s, Random r, int x, int y) =>
                r.NextDouble() < 0.1 && s.getDistanceFromStart(x, y) > 10.0;
            bool SkeletonPredicate(MineShaft s, Random r, int x, int y) => r.NextDouble() < 0.7;
            bool DustSpiritPredicate(MineShaft s, Random r, int x, int y) => r.NextDouble() < 0.3;
            bool FrostBatPredicate(MineShaft s, Random r, int x, int y) =>
                r.NextDouble() < 0.3 && s.getDistanceFromStart(x, y) > 10.0;
            bool GhostPredicate(MineShaft s, Random r, int x, int y) =>
                !s.wasGhostAdded() && (r.NextDouble() < 0.3 && s.getDistanceFromStart(x, y) > 10.0);
            bool LavaBatPredicate(MineShaft s, Random r, int x, int y) => s.isDarkArea() && r.NextDouble() < 0.25;
            bool RedSlimePredicate(MineShaft s, Random r, int x, int y) => r.NextDouble() < 0.15;
            bool MetalHeadPredicate(MineShaft s, Random r, int x, int y) => r.NextDouble() < 0.15;
            bool ShadowBrutePredicate(MineShaft s, Random r, int x, int y) => r.NextDouble() < 0.25;
            bool ShadowShamanPredicate(MineShaft s, Random r, int x, int y) => r.NextDouble() < 0.25;
            bool LavaCrabPredicate(MineShaft s, Random r, int x, int y) => r.NextDouble() < 0.25;
            bool SquidKidPredicate(MineShaft s, Random r, int x, int y) =>
                r.NextDouble() < 0.2 && s.getDistanceFromStart(x, y) > 8.0;

            // SPECIAL - Slime Infestation
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MineshaftArea.SlimeArea));
            Register(MineshaftArea.SlimeArea,
                new MineshaftSpawnChance(new RegistryKey("big_slime"), 0, 0, 1, InfestedBigSlimePredicate));
            Register(MineshaftArea.SlimeArea, new MineshaftSpawnChance(new RegistryKey("green_slime"), 0, 0, 2));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MineshaftArea.SlimeArea));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MineshaftArea.SlimeArea));
            
            // SPECIAL - Dino Area
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MineshaftArea.DinoArea));
            Register(MineshaftArea.DinoArea,
                new MineshaftSpawnChance(new RegistryKey("iridium_bat"), 0, 0, 1, DinoAreaPredicate));
            Register(MineshaftArea.DinoArea,
                new MineshaftSpawnChance(new RegistryKey("mutant_fly"), 0, 0, 2, DinoAreaPredicate));
            Register(MineshaftArea.DinoArea, new MineshaftSpawnChance(new RegistryKey("pepper_rex"), 0, 0, 999));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MineshaftArea.DinoArea));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MineshaftArea.DinoArea));
            
            // Floors 0 - 9 - Surface
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MineshaftArea.Surface));
            Register(MineshaftArea.Surface,
                new MineshaftSpawnChance(new RegistryKey("bug"), 0, 9, -1, RandomBugPredicate));
            Register(MineshaftArea.Surface,
                new MineshaftSpawnChance(new RegistryKey("duggy"), 0, 9, 1, DuggyPredicate));
            Register(MineshaftArea.Surface,
                new MineshaftSpawnChance(new RegistryKey("rock_crab"), 0, 9, 2, RockCrabPredicate));
            Register(MineshaftArea.Surface, new MineshaftSpawnChance(new RegistryKey("green_slime"), 0, 9, 999));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MineshaftArea.Surface));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MineshaftArea.Surface));

            // Floors 10 - 30 - Underground
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MineshaftArea.Underground));
            Register(MineshaftArea.Underground,
                new MineshaftSpawnChance(new RegistryKey("bug"), 10, 30, -1, RandomBugPredicate));
            Register(MineshaftArea.Underground,
                new MineshaftSpawnChance(new RegistryKey("duggy"), 10, 30, 1, DuggyPredicate));
            Register(MineshaftArea.Underground,
                new MineshaftSpawnChance(new RegistryKey("rock_crab"), 10, 30, 2, RockCrabPredicate));
            Register(MineshaftArea.Underground,
                new MineshaftSpawnChance(new RegistryKey("green_slime"), 10, 14, 999));
            Register(MineshaftArea.Underground,
                new MineshaftSpawnChance(new RegistryKey("fly"), 15, 30, 3, FlyPredicate));
            Register(MineshaftArea.Underground,
                new MineshaftSpawnChance(new RegistryKey("green_slime"), 15, 30, 4, GreenSlimePredicate));
            Register(MineshaftArea.Underground, new MineshaftSpawnChance(new RegistryKey("grub"), 15, 30, 999));

            // Floors 30 - 40 - Shadow
            Register(MineshaftArea.Underground,
                new MineshaftSpawnChance(new RegistryKey("bat"), 30, 40, 1, BatPredicate));
            Register(MineshaftArea.Underground, new MineshaftSpawnChance(new RegistryKey("rock_golem"), 30, 40, 2));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MineshaftArea.Underground));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MineshaftArea.Underground));
            
            // Floors 40 - 80 - Ice
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MineshaftArea.Ice));
            Register(MineshaftArea.Ice,
                new MineshaftSpawnChance(new RegistryKey("skeleton"), 70, 80, 1, SkeletonPredicate));
            Register(MineshaftArea.Ice,
                new MineshaftSpawnChance(new RegistryKey("dust_spirit"), 40, 80, 2, DustSpiritPredicate));
            Register(MineshaftArea.Ice,
                new MineshaftSpawnChance(new RegistryKey("bat"), 40, 80, 3, FrostBatPredicate));
            Register(MineshaftArea.Ice,
                new MineshaftSpawnChance(new RegistryKey("ghost"), 50, 80, 4, GhostPredicate));
            Register(MineshaftArea.Ice,
                new MineshaftSpawnChance(new RegistryKey("green_slime"), 40, 80, 999));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MineshaftArea.Ice));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MineshaftArea.Ice));

            // Floors 80 - 120 - Lava
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MineshaftArea.Lava));
            Register(MineshaftArea.Lava,
                new MineshaftSpawnChance(new RegistryKey("bat"), 80, 120, 1, LavaBatPredicate));
            Register(MineshaftArea.Lava,
                new MineshaftSpawnChance(new RegistryKey("green_slime"), 80, 120, 2, RedSlimePredicate));
            Register(MineshaftArea.Lava,
                new MineshaftSpawnChance(new RegistryKey("metal_head"), 80, 120, 3, MetalHeadPredicate));
            Register(MineshaftArea.Lava,
                new MineshaftSpawnChance(new RegistryKey("shadow_brute"), 80, 120, 4, ShadowBrutePredicate));
            Register(MineshaftArea.Lava,
                new MineshaftSpawnChance(new RegistryKey("shadow_shaman"), 80, 120, 5, ShadowShamanPredicate));
            Register(MineshaftArea.Lava,
                new MineshaftSpawnChance(new RegistryKey("lava_crab"), 80, 120, 6, LavaCrabPredicate));
            Register(MineshaftArea.Lava,
                new MineshaftSpawnChance(new RegistryKey("squid_kid"), 90, 120, 7, SquidKidPredicate));
            Register(MineshaftArea.Lava,
                new MineshaftSpawnChance(new RegistryKey("green_slime"), 80, 120, 999));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MineshaftArea.Lava));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MineshaftArea.Lava));
        }
    }
}