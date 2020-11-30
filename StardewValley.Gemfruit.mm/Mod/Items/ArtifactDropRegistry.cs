using System;
using System.Collections.Generic;
using System.Linq;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Events;
using Gemfruit.Mod.API.Events.Items;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Internal.Exceptions;

namespace Gemfruit.Mod.Items
{
    public class ArtifactDropRegistry : PhasableRegistry
    {
        private static readonly Dictionary<ResourceKey, List<ArtifactDropData>> DropList
            = new Dictionary<ResourceKey, List<ArtifactDropData>>();

        private static readonly Dictionary<ResourceKey, Dictionary<ResourceKey, double>> VanillaPairs
            = new Dictionary<ResourceKey, Dictionary<ResourceKey, double>>();

        protected override void InitializeRecords()
        {
            foreach (var pair in VanillaPairs)
            {
                Register(pair.Key,
                    pair.Value.Aggregate(new ArtifactDropDataBuilder(),
                        (current, loc) => current.AddChance(loc.Key, loc.Value)));
            }

            GemfruitMod.InitBus.FireEvent(new ArtifactDropRegistrationEvent(this, EventPhase.During));
            GemfruitMod.InitBus.FireEvent(new ArtifactDropRegistrationEvent(this, EventPhase.After));
        }

        internal void AddVanillaItem(ResourceKey item, string line)
        {
            var parts = line.Split('/');
            if (parts.Length >= 7)
            {
                var chances = parts[6].Split(' ');
                if (chances.Length % 2 == 0)
                {
                    VanillaPairs.Add(item, new Dictionary<ResourceKey, double>());
                    for (var i = 0; i < chances.Length; i += 2)
                    {
                        var loc = new ResourceKey(StringUtility.SanitizeName(chances[i]));
                        var pct = Convert.ToDouble(chances[i + 1]);

                        VanillaPairs[item].Add(loc, pct);
                    }
                }
                else
                {
                    throw new Exception($"BAD ARTIFACT CHANCE LENGTH '{chances.Length}'");
                }
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.Warning, GetType().Name,
                    $"For some reason, '{item}' doesn't have a chance as an artifact");
            }
        }

        public void Register(ResourceKey item, ArtifactDropDataBuilder builder)
        {
            if (CurrentPhase == RegistryPhase.Open)
            {
                if (DropList.ContainsKey(item))
                {
                    throw new RegistryConflictException(item);
                }

                GemfruitMod.Logger.Log(LogLevel.Debug, GetType().Name,
                    $"Registering artifact drop '{item}'");

                foreach (var loc in builder.DropChances.Keys)
                {
                    if (!DropList.ContainsKey(loc))
                    {
                        DropList.Add(loc, new List<ArtifactDropData>());
                    }

                    DropList[loc].Add(new ArtifactDropData(item, builder.DropChances[loc]));
                }
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.Error, GetType().Name,
                    $"Attempted to register '{item}' before/after corresponding lifecycle event!");
            }
        }
    }
}