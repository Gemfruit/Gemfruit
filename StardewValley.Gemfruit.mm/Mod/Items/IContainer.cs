using System.Collections.Generic;

namespace Gemfruit.Mod.Items
{
    public interface IContainer
    { 
        void DeserializeFrom(IReadOnlyDictionary<string, object> dict);
        void SerializeTo(IDictionary<string, object> dict);
    }
}