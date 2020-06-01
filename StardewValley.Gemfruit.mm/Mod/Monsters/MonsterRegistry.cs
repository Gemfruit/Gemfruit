using System.Collections.Generic;
using System.Diagnostics;
using Gemfruit.Mod.Events.Monsters;
using Gemfruit.Mod.Internal;
using StardewValley.Locations;
using StardewValley.Monsters;

namespace Gemfruit.Mod.Monsters
{
    public class MonsterRegistry
    {
        private readonly Dictionary<RegistryKey, MonsterType> _types =
            new Dictionary<RegistryKey, MonsterType>();

        private RegistryPhase _currentPhase = RegistryPhase.Closed;

        public void Register(MonsterType value)
        {
            if (value == null)
            {
                GemfruitMod.Logger.Log(LogLevel.ERROR, "MonsterRegistry",
                    "Attempted to register a MonsterType without a value!");
            }
            else
            {
                if (_currentPhase == RegistryPhase.Open)
                {
                    GemfruitMod.Logger.Log(LogLevel.DEBUG, "MonsterRegistry", $"Adding type for {value.getName()}");
                    _types.Add(value.getName(), value);
                }
                else
                {
                    GemfruitMod.Logger.Log(LogLevel.ERROR, "MonsterRegistry",
                        $"Attempted to register monster '{value.getName()}' before corresponding lifecycle event!");
                }
            }
        }

        public Optional<MonsterType> Get(RegistryKey key)
        {
            if (_currentPhase == RegistryPhase.Frozen)
            {
                if (_types.ContainsKey(key))
                    return new Optional<MonsterType>(_types[key]);
                
                GemfruitMod.Logger.Log(LogLevel.ERROR, "MonsterRegistry",
                    $"Attempted to get non-existent monster '{key}'!");
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.ERROR, "MonsterRegistry",
                    $"Attempted to get monster '{key}' before registration was done!");
            }

            return Optional<MonsterType>.None();
        }

        public void Initialize()
        {
            _currentPhase = RegistryPhase.Open;
            GemfruitMod.InitBus.FireEvent(new MonsterRegistrationEvent(this, EventPhase.Before));
            Register(new MonsterType(new RegistryKey("green_slime"))
                .setMineshaftConstructor(data => new GreenSlime(data.Position, data.Level)));
            Register(new MonsterType(new RegistryKey("big_slime"))
                .setMineshaftConstructor(data => new BigSlime(data.Position, data.Area)));
            Register(new MonsterType(new RegistryKey("bug"))
                .setMineshaftConstructor(data => new Bug(data.Position,
                    data.Rand.Next(4), data.Shaft)));
            Register(new MonsterType(new RegistryKey("duggy"))
                .setMineshaftConstructor(data => new Duggy(data.Position)));
            Register(new MonsterType(new RegistryKey("rock_crab"))
                .setMineshaftConstructor(data => new RockCrab(data.Position)));
            Register(new MonsterType(new RegistryKey("fly"))
                .setMineshaftConstructor(data => new Fly(data.Position)));
            Register(new MonsterType(new RegistryKey("grub"))
                .setMineshaftConstructor(data => new Grub(data.Position)));
            Register(new MonsterType(new RegistryKey("bat"))
                .setMineshaftConstructor(data => new Bat(data.Position, data.Level)));
            Register(new MonsterType(new RegistryKey("rock_golem"))
                .setMineshaftConstructor(data => new RockGolem(data.Position, data.Shaft)));
            Register(new MonsterType(new RegistryKey("skeleton"))
                .setMineshaftConstructor(data => new Skeleton(data.Position)));
            Register(new MonsterType(new RegistryKey("dust_spirit"))
                .setMineshaftConstructor(data => new DustSpirit(data.Position, 
                    data.Rand.NextDouble() < 0.8)));
            Register(new MonsterType(new RegistryKey("ghost"))
                .setMineshaftConstructor(
                    data =>
                    {
                        data.Shaft.setGhostAdded(true);
                        return new Ghost(data.Position);
                    }));
            Register(new MonsterType(new RegistryKey("metal_head"))
                .setMineshaftConstructor(data => new MetalHead(data.Position, data.Area)));
            Register(new MonsterType(new RegistryKey("shadow_brute"))
                .setMineshaftConstructor(data => new ShadowBrute(data.Position)));
            Register(new MonsterType(new RegistryKey("shadow_shaman"))
                .setMineshaftConstructor(data => new ShadowShaman(data.Position)));
            Register(new MonsterType(new RegistryKey("lava_crab"))
                .setMineshaftConstructor(data => new RockCrab(data.Position, "Lava Crab")));
            Register(new MonsterType(new RegistryKey("squid_kid"))
                .setMineshaftConstructor(data => new SquidKid(data.Position)));
            
            Register(new MonsterType(new RegistryKey("wild_shadow_brute"))
                .setWildernessConstructor(
                    data => new ShadowBrute(data.position)
                    {
                        focusedOnFarmers = true, wildernessFarmMonster = true
                    }));
            Register(new MonsterType(new RegistryKey("wild_golem"))
                .setWildernessConstructor(
                    data => new RockGolem(data.position, data.player.combatLevel)
                    {
                        focusedOnFarmers = true, wildernessFarmMonster = true
                    }));
            Register(new MonsterType(new RegistryKey("wild_slime"))
                .setWildernessConstructor(
                    data =>
                    {
                        var mineLevel = 1;
                        if (data.player.combatLevel >= 10) mineLevel = 140;
                        else if (data.player.combatLevel >= 8) mineLevel = 100;
                        else if (data.player.combatLevel >= 4) mineLevel = 41;
                        return new GreenSlime(data.position, mineLevel)
                        {
                            wildernessFarmMonster = true
                        };
                    }));
            Register(new MonsterType(new RegistryKey("galaxy_bat"))
                .setWildernessConstructor(
                    data => new Bat(data.position, 9999)
                    {
                        focusedOnFarmers = true,
                        wildernessFarmMonster = true
                    }));
            Register(new MonsterType(new RegistryKey("iridium_bat"))
                .setWildernessConstructor(
                    data => new Bat(data.position, 172)
                    {
                        focusedOnFarmers = true,
                        wildernessFarmMonster = true
                    }));
            Register(new MonsterType(new RegistryKey("wild_serpent"))
                .setWildernessConstructor(
                    data => new Serpent(data.position)
                    {
                        focusedOnFarmers = true,
                        wildernessFarmMonster = true
                    }));
            Register(new MonsterType(new RegistryKey("lava_bat"))
                .setWildernessConstructor(
                    data => new Bat(data.position, 81)
                    {
                        focusedOnFarmers = true,
                        wildernessFarmMonster = true
                    }));
            Register(new MonsterType(new RegistryKey("frost_bat"))
                .setWildernessConstructor(
                    data => new Bat(data.position, 41)
                    {
                        focusedOnFarmers = true,
                        wildernessFarmMonster = true
                    }));
            Register(new MonsterType(new RegistryKey("wild_bat"))
                .setWildernessConstructor(
                    data => new Bat(data.position, 1)
                    {
                        focusedOnFarmers = true,
                        wildernessFarmMonster = true
                    }));
            GemfruitMod.InitBus.FireEvent(new MonsterRegistrationEvent(this, EventPhase.During));
            GemfruitMod.InitBus.FireEvent(new MonsterRegistrationEvent(this, EventPhase.After));
            _currentPhase = RegistryPhase.Frozen;
        }
    }
}