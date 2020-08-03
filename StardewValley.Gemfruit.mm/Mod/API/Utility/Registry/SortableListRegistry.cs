using System;
using System.Collections.Generic;
using Gemfruit.Mod.Internal;

namespace Gemfruit.Mod.API.Utility.Registry
{
    public abstract class SortableListRegistry<TEnum, TValue> : PhasableRegistry
        where TEnum: Enum
        where TValue: IPrioritizable
    {
        protected readonly Dictionary<TEnum, List<TValue>> _dictionary;

        public SortableListRegistry()
        {
            _dictionary = new Dictionary<TEnum, List<TValue>>();
            foreach (var m in Enum.GetValues(typeof(TEnum)))
            {
                _dictionary[(TEnum) m] = new List<TValue>();
            }
        }
        
        public void Register(TEnum key, TValue value)
        {
            if (CurrentPhase == RegistryPhase.Open)
            {
                GemfruitMod.Logger.Log(LogLevel.DEBUG, GetType().Name,
                    $"Registering value '{value}' @ '{key}'");
                _dictionary[key].Add(value);
                _dictionary[key].Sort((x, y) => x.Priority - y.Priority);
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.ERROR, GetType().Name,
                    $"Attempted to register value '{value}' before corresponding lifecycle event!");
            }
        }
    }
}