using System;
using System.Collections.Generic;
using System.Linq;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Internal.Transformers;

namespace Gemfruit.Mod.Items
{
    public class GeodeResultRegistry : KeyedRegistry<GeodeResultData>
    {
        private static readonly Dictionary<ResourceKey, GeodeResultData> ResultList
            = new Dictionary<ResourceKey, GeodeResultData>();
        
        private static readonly Dictionary<ResourceKey, GeodeResultData> VanillaGeodes
            = new Dictionary<ResourceKey, GeodeResultData>();
        
        internal void ParseVanillaItem(ResourceKey item, string line, Dictionary<int, string> lines)
        {
            var items = new List<ResourceKey>();
            var parts = line.Split('/');
            if (parts.Length >= 7)
            {
                try
                {
                    var ids = parts[6].Split().Select(int.Parse).ToList();
                    items.AddRange(from i in ids
                        let rname = lines[i].Split('/')[0]
                        select VanillaResourceKeyTransformers.ApplyTransformerForKey(null, i,
                            new ResourceKey(StringUtility.SanitizeName(rname))));
                    VanillaGeodes.Add(item, new GeodeResultData(items));
                }
                catch (Exception e)
                {
                    GemfruitMod.Logger.Log(LogLevel.Error, GetType().Name,
                        $"error registering '{item}' geode drops - {e.Message}");
                    GemfruitMod.Logger.Log(LogLevel.Error, GetType().Name,
                        e.StackTrace);
                }
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.Warning, GetType().Name,
                    $"for some reason, '{item}' doesn't look like a Geode");
            }
        }
        
        protected override void InitializeRecords()
        {
            VanillaRegistration();
            // TODO: Fire Events
        }

        private void VanillaRegistration()
        {
            foreach (var pair in VanillaGeodes)
            {
                Register(pair.Key, pair.Value);
            }
        }

        protected override void AddItem(ResourceKey key, GeodeResultData data)
        {
            ResultList.Add(key, data);
        }

        protected override bool HasKey(ResourceKey key)
        {
            return ResultList.ContainsKey(key);
        }
    }
}