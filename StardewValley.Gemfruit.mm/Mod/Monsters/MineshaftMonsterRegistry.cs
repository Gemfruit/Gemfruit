using System;
using System.Collections.Generic;
using Gemfruit.Mod.Events.Monsters;
using Gemfruit.Mod.Internal;
using Microsoft.Xna.Framework;
using StardewValley.Locations;
using StardewValley.Monsters;

namespace Gemfruit.Mod.Monsters
{
    public class MineshaftMonsterRegistry
    {
        public struct SpawnData
        {
            public readonly MineShaft Shaft;
            public readonly Random Rand;
            public readonly Vector2 Position;
            public readonly int Area;
            public readonly int Level;

            public SpawnData(MineShaft shaft, Random rand, Vector2 position, int area, int level)
            {
                Shaft = shaft;
                Rand = rand;
                Position = position;
                Area = area;
                Level = level;
            }
        }
    
        public delegate Monster MonsterConstructor(
            SpawnData mineshaftSpawnData);

        
        private readonly Dictionary<RegistryKey, MonsterConstructor> _constructors =
            new Dictionary<RegistryKey, MonsterConstructor>();

        private RegistryPhase _currentPhase = RegistryPhase.Closed;

        public void Register(RegistryKey key, MonsterConstructor value)
        {
            if (_currentPhase == RegistryPhase.Open)
            {
                GemfruitMod.Logger.Log(LogLevel.DEBUG, "MineshaftMonsterRegistry", $"Adding constructor for {key}");
                _constructors.Add(key, value);
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.ERROR, "MineshaftMonsterRegistry",
                    $"Attempted to register monster '{key}' before corresponding lifecycle event!");
            }
        }

        public Optional<MonsterConstructor> Get(RegistryKey key)
        {
            if (_currentPhase == RegistryPhase.Frozen)
            {
                if (_constructors.ContainsKey(key))
                    return new Optional<MonsterConstructor>(_constructors[key]);
                
                GemfruitMod.Logger.Log(LogLevel.ERROR, "MonsterRegistry",
                    $"Attempted to get non-existent monster '{key}'!");
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.ERROR, "MonsterRegistry",
                    $"Attempted to get monster '{key}' before registration was done!");
            }

            return Optional<MonsterConstructor>.None();
        }

        public void Initialize()
        {
            _currentPhase = RegistryPhase.Open;
            GemfruitMod.InitBus.FireEvent(new MineshaftMonsterRegistrationEvent(this, EventPhase.Before));
            Register(new RegistryKey("green_slime"), data => new GreenSlime(data.Position, data.Level));
            Register(new RegistryKey("big_slime"), data => new BigSlime(data.Position, data.Area));
            Register(new RegistryKey("bug"),
                data => new Bug(data.Position, data.Rand.Next(4), data.Shaft));
            Register(new RegistryKey("duggy"), data => new Duggy(data.Position));
            Register(new RegistryKey("rock_crab"), data => new RockCrab(data.Position));
            Register(new RegistryKey("fly"), data => new Fly(data.Position));
            Register(new RegistryKey("grub"), data => new Grub(data.Position));
            Register(new RegistryKey("bat"), data => new Bat(data.Position, data.Level));
            Register(new RegistryKey("rock_golem"), data => new RockGolem(data.Position, data.Shaft));
            Register(new RegistryKey("skeleton"), data => new Skeleton(data.Position));
            Register(new RegistryKey("dust_spirit"),
                data => new DustSpirit(data.Position, data.Rand.NextDouble() < 0.8));
            Register(new RegistryKey("ghost"), data =>
            {
                data.Shaft.setGhostAdded(true);
                return new Ghost(data.Position);
            });
            Register(new RegistryKey("metal_head"), data => new MetalHead(data.Position, data.Area));
            Register(new RegistryKey("shadow_brute"), data => new ShadowBrute(data.Position));
            Register(new RegistryKey("shadow_shaman"), data => new ShadowShaman(data.Position));
            Register(new RegistryKey("lava_crab"), data => new RockCrab(data.Position, "Lava Crab"));
            Register(new RegistryKey("squid_kid"), data => new SquidKid(data.Position));
            GemfruitMod.InitBus.FireEvent(new MineshaftMonsterRegistrationEvent(this, EventPhase.During));
            GemfruitMod.InitBus.FireEvent(new MineshaftMonsterRegistrationEvent(this, EventPhase.After));
            _currentPhase = RegistryPhase.Frozen;
        }
    }
}