using System;
using System.Collections.Generic;
using Gemfruit.Mod.Internal;

namespace Gemfruit.Mod.API.Utility.Registry
{
    public abstract class MultiSortableListRegistry<TEnum, TPartition, TValue> : PhasableRegistry
        where TEnum: Enum
        where TPartition: Enum
        where TValue: IPrioritizable
    {
        protected readonly Dictionary<TPartition, Dictionary<TEnum, List<TValue>>> Dictionary;

        public MultiSortableListRegistry()
        {
            Dictionary = new Dictionary<TPartition, Dictionary<TEnum, List<TValue>>>();
            foreach (var p in Enum.GetValues(typeof(TPartition)))
            {
                Dictionary[(TPartition) p] = new Dictionary<TEnum, List<TValue>>();
                foreach (var m in Enum.GetValues(typeof(TEnum)))
                {
                    Dictionary[(TPartition) p][(TEnum) m] = new List<TValue>();
                }
            }
        }

        public void Register(TPartition partition, TEnum key,  TValue value)
        {
            if (CurrentPhase == RegistryPhase.Open)
            {
                GemfruitMod.Logger.Log(LogLevel.Debug, GetType().Name,
                    $"Registering value '{value}' @ '{partition}::{key}'");
                Dictionary[partition][key].Add(value);
                Dictionary[partition][key].Sort((x, y) => x.Priority - y.Priority);
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.Error, GetType().Name,
                    $"Attempted to register value '{value}' before corresponding lifecycle event!");
            }
        }
    }
}