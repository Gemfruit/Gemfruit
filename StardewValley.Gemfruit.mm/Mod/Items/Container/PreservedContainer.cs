using System.Collections.Generic;
using Gemfruit.Mod.API;
using Gemfruit.Mod.Internal;

namespace Gemfruit.Mod.Items.Container
{
    public class PreservedContainer : IContainer
    {
        public ResourceKey PreservableItem { get; protected set; }

        public void DeserializeFrom(IReadOnlyDictionary<string, object> dict)
        {
            object tmp;
            if (dict.TryGetValue("preservable_item", out tmp))
            {
                if (tmp is ResourceKey key)
                {
                    PreservableItem = key;
                }
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.Error, "PreservedContainer", 
                    "Requested deserialization of preserved container failed! Key not found!");
            }
        }

        public void SerializeTo(IDictionary<string, object> dict)
        {
            dict.Add("preservable_item", PreservableItem);
        }
    }
}