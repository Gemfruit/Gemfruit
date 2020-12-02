using System;
using System.Collections.Generic;
using System.Linq;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Events;
using Gemfruit.Mod.API.Events.Items;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Internal;

namespace Gemfruit.Mod.Items
{
    public class ArtifactDropRegistry : KeyedRegistry<ArtifactDropDataBuilder>
    {
        private static readonly Dictionary<ResourceKey, List<ArtifactDropData>> DropList
            = new Dictionary<ResourceKey, List<ArtifactDropData>>();

        private static readonly Dictionary<ResourceKey, Dictionary<ResourceKey, double>> VanillaPairs
            = new Dictionary<ResourceKey, Dictionary<ResourceKey, double>>();

        protected override bool HasKey(ResourceKey key)
        {
            return false;
        }

        protected override void AddItem(ResourceKey key, ArtifactDropDataBuilder builder)
        {
            foreach (var loc in builder.DropChances.Keys)
            {
                if (!DropList.ContainsKey(loc))
                {
                    DropList.Add(loc, new List<ArtifactDropData>());
                }

                DropList[loc].Add(new ArtifactDropData(key, builder.DropChances[loc]));
            }
        }

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

        internal void ParseVanillaItem(ResourceKey item, string line)
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
                else if(chances[0] != "")
                {
                    throw new Exception($"for some reason, '{item}' has an uneven number of artifact chances - '{chances.Length}'");
                }
                else
                {
                    GemfruitMod.Logger.Log(LogLevel.Warning, GetType().Name,
                        $"for some reason, '{item}' doesn't has an empty chance def");
                }
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.Warning, GetType().Name,
                    $"for some reason, '{item}' doesn't have a chance as an artifact");
            }
        }
    }
}