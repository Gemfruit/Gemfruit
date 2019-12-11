using System;
using System.Collections.Generic;

namespace Gemfruit.Mod.Internal
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
        
        public void Register(TEnum area, TValue chance)
        {
            if (CurrentPhase == RegistryPhase.Open)
            {
                GemfruitMod.Logger.Log(LogLevel.DEBUG, GetType().Name,
                    $"Adding spawn chance for {chance}");
                _dictionary[area].Add(chance);
                _dictionary[area].Sort((x, y) => x.Priority - y.Priority);
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.ERROR, GetType().Name,
                    $"Attempted to register spawn for '{chance}' before corresponding lifecycle event!");
            }
        }
    }
}