using System.Collections.Generic;
using Gemfruit.Mod.API.Errors;
using Gemfruit.Mod.API.Utility;

namespace Gemfruit.Mod.Items.Container
{
    public interface IContainer
    { 
        Optional<Error> DeserializeFrom(IReadOnlyDictionary<string, object> dict);
        Optional<Error> SerializeTo(IDictionary<string, object> dict);
    }
}