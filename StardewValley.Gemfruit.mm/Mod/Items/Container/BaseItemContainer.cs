using System.Collections.Generic;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Errors;
using Gemfruit.Mod.API.Utility;
using Gemfruit.Mod.Internal;

namespace Gemfruit.Mod.Items.Container
{
    public class BaseItemContainer : IContainer
    {
        public ResourceKey BaseItem { get; set; }

        public Optional<Error> DeserializeFrom(IReadOnlyDictionary<string, object> dict)
        {
            // TODO: A Deserialization API should probably just have functions to auto-magically handle errors like this.
            if (dict.TryGetValue("base_item", out var tmp))
            {
                if (tmp is ResourceKey key)
                {
                    BaseItem = key;
                }
                else
                {
                    return new Optional<Error>(new Error(this, $"Value of key 'base_item' not a ResourceKey!"));
                }
            }
            else
            {
               return new Optional<Error>(new Error(this, $"Key 'base_item' not found!"));
            }
            return Optional<Error>.None();
        }

        public Optional<Error> SerializeTo(IDictionary<string, object> dict)
        {
            dict.Add("base_item", BaseItem);
            return Optional<Error>.None();
        }
    }
}