using System;
using System.Collections.Generic;
using Gemfruit.Mod.Internal;
using StardewValley;
using StardewValley.Monsters;

namespace Gemfruit.Mod.Monsters
{
    public class WildernessMonsterRegistry
    {
        public struct SpawnData
        {
            public readonly Farm farm;
            public readonly Farmer player;
            public readonly Random rand;
        }
        
        public delegate Monster MonsterConstructor(SpawnData farmSpawnData);
        
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
            
        }
    }
}