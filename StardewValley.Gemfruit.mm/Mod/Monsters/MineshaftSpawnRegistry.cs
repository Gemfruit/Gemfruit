using System;
using System.Linq;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Events;
using Gemfruit.Mod.API.Events.Monsters;
using Gemfruit.Mod.API.Utility;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Internal;
using StardewValley.Locations;

namespace Gemfruit.Mod.Monsters
{
    public class MineshaftSpawnRegistry : MultiSortableListRegistry<MineshaftArea, MonsterLocomotion, MineshaftSpawnChance>
    {
        public Optional<ResourceKey> Get(MonsterLocomotion type, MineshaftArea area, MineShaft shaft, Random rand, int level, int xTile, int yTile)
        {
            if (CurrentPhase == RegistryPhase.Frozen)
            {
                foreach (var p in Dictionary[type][area].Where(p => p.Evaluate(shaft, rand, level, xTile, yTile)))
                {
                    return new Optional<ResourceKey>(p.Monster);
                }
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.Error, "MineshaftSpawnRegistry",
                    $"Attempted to get spawn for area '{area}' before registration was done!");
            }

            return Optional<ResourceKey>.None();
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
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MonsterLocomotion.Ground, MineshaftArea.SlimeArea));
            Register(MonsterLocomotion.Ground, MineshaftArea.SlimeArea,
                new MineshaftSpawnChance(new ResourceKey("big_slime"), 0, 0, 1, InfestedBigSlimePredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.SlimeArea,
                new MineshaftSpawnChance(new ResourceKey("green_slime"), 0, 0, 2));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MonsterLocomotion.Ground, MineshaftArea.SlimeArea));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MonsterLocomotion.Ground, MineshaftArea.SlimeArea));
            
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MonsterLocomotion.Flying, MineshaftArea.SlimeArea));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MonsterLocomotion.Flying, MineshaftArea.SlimeArea));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MonsterLocomotion.Flying, MineshaftArea.SlimeArea));
           
            // SPECIAL - Dino Area
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MonsterLocomotion.Ground, MineshaftArea.DinoArea));
            Register(MonsterLocomotion.Ground, MineshaftArea.DinoArea,
                new MineshaftSpawnChance(new ResourceKey("iridium_bat"), 0, 0, 1, DinoAreaPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.DinoArea,
                new MineshaftSpawnChance(new ResourceKey("mutant_fly"), 0, 0, 2, DinoAreaPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.DinoArea, new MineshaftSpawnChance(new ResourceKey("pepper_rex"), 0, 0, 999));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MonsterLocomotion.Ground, MineshaftArea.DinoArea));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MonsterLocomotion.Ground, MineshaftArea.DinoArea));
            
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MonsterLocomotion.Flying, MineshaftArea.DinoArea));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MonsterLocomotion.Flying, MineshaftArea.DinoArea));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MonsterLocomotion.Flying, MineshaftArea.DinoArea));

            
            // Floors 0 - 9 - Surface
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MonsterLocomotion.Ground, MineshaftArea.Surface));
            Register(MonsterLocomotion.Ground, MineshaftArea.Surface,
                new MineshaftSpawnChance(new ResourceKey("bug"), 0, 9, -1, RandomBugPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Surface,
                new MineshaftSpawnChance(new ResourceKey("duggy"), 0, 9, 1, DuggyPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Surface,
                new MineshaftSpawnChance(new ResourceKey("rock_crab"), 0, 9, 2, RockCrabPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Surface, new MineshaftSpawnChance(new ResourceKey("green_slime"), 0, 9, 999));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MonsterLocomotion.Ground, MineshaftArea.Surface));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MonsterLocomotion.Ground, MineshaftArea.Surface));

            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MonsterLocomotion.Flying, MineshaftArea.Surface));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MonsterLocomotion.Flying, MineshaftArea.Surface));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MonsterLocomotion.Flying, MineshaftArea.Surface));
            
            // Floors 10 - 30 - Underground
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MonsterLocomotion.Ground, MineshaftArea.Underground));
            Register(MonsterLocomotion.Ground, MineshaftArea.Underground,
                new MineshaftSpawnChance(new ResourceKey("bug"), 10, 30, -1, RandomBugPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Underground,
                new MineshaftSpawnChance(new ResourceKey("duggy"), 10, 30, 1, DuggyPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Underground,
                new MineshaftSpawnChance(new ResourceKey("rock_crab"), 10, 30, 2, RockCrabPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Underground,
                new MineshaftSpawnChance(new ResourceKey("green_slime"), 10, 14, 999));
            Register(MonsterLocomotion.Ground, MineshaftArea.Underground,
                new MineshaftSpawnChance(new ResourceKey("fly"), 15, 30, 3, FlyPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Underground,
                new MineshaftSpawnChance(new ResourceKey("green_slime"), 15, 30, 4, GreenSlimePredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Underground, new MineshaftSpawnChance(new ResourceKey("grub"), 15, 30, 999));

            // Floors 30 - 40 - Shadow
            Register(MonsterLocomotion.Ground, MineshaftArea.Underground,
                new MineshaftSpawnChance(new ResourceKey("bat"), 30, 40, 1, BatPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Underground, 
                new MineshaftSpawnChance(new ResourceKey("rock_golem"), 30, 40, 2));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MonsterLocomotion.Ground, MineshaftArea.Underground));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MonsterLocomotion.Ground, MineshaftArea.Underground));
            
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MonsterLocomotion.Flying, MineshaftArea.Underground));
            Register(MonsterLocomotion.Flying, MineshaftArea.Underground,
                new MineshaftSpawnChance(new ResourceKey("flying_fly"), 10, 40, 999));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MonsterLocomotion.Flying, MineshaftArea.Underground));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MonsterLocomotion.Flying, MineshaftArea.Underground));
            
            // Floors 40 - 80 - Ice
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MonsterLocomotion.Ground, MineshaftArea.Ice));
            Register(MonsterLocomotion.Ground, MineshaftArea.Ice,
                new MineshaftSpawnChance(new ResourceKey("skeleton"), 70, 80, 1, SkeletonPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Ice,
                new MineshaftSpawnChance(new ResourceKey("dust_spirit"), 40, 80, 2, DustSpiritPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Ice,
                new MineshaftSpawnChance(new ResourceKey("bat"), 40, 80, 3, FrostBatPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Ice,
                new MineshaftSpawnChance(new ResourceKey("ghost"), 50, 80, 4, GhostPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Ice,
                new MineshaftSpawnChance(new ResourceKey("green_slime"), 40, 80, 999));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MonsterLocomotion.Ground, MineshaftArea.Ice));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MonsterLocomotion.Ground, MineshaftArea.Ice));

            // Floors 80 - 120 - Lava
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.Before, MonsterLocomotion.Ground, MineshaftArea.Lava));
            Register(MonsterLocomotion.Ground, MineshaftArea.Lava,
                new MineshaftSpawnChance(new ResourceKey("bat"), 80, 120, 1, LavaBatPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Lava,
                new MineshaftSpawnChance(new ResourceKey("green_slime"), 80, 120, 2, RedSlimePredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Lava,
                new MineshaftSpawnChance(new ResourceKey("metal_head"), 80, 120, 3, MetalHeadPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Lava,
                new MineshaftSpawnChance(new ResourceKey("shadow_brute"), 80, 120, 4, ShadowBrutePredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Lava,
                new MineshaftSpawnChance(new ResourceKey("shadow_shaman"), 80, 120, 5, ShadowShamanPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Lava,
                new MineshaftSpawnChance(new ResourceKey("lava_crab"), 80, 120, 6, LavaCrabPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Lava,
                new MineshaftSpawnChance(new ResourceKey("squid_kid"), 90, 120, 7, SquidKidPredicate));
            Register(MonsterLocomotion.Ground, MineshaftArea.Lava,
                new MineshaftSpawnChance(new ResourceKey("green_slime"), 80, 120, 999));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.During, MonsterLocomotion.Ground, MineshaftArea.Lava));
            GemfruitMod.InitBus.FireEvent(new MineshaftSpawnRegistrationEvent(this, EventPhase.After, MonsterLocomotion.Ground, MineshaftArea.Lava));
        }
    }
}