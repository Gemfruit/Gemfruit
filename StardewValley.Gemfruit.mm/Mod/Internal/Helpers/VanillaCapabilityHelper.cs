using System;
using Gemfruit.Mod.API;
using Gemfruit.Mod.Items;
using Gemfruit.Mod.Items.Capabilities;

namespace Gemfruit.Mod.Internal.Helpers
{
    internal static class VanillaCapabilityHelper
    {
        internal static Action<IHasItemCapabilities> Seedable(ResourceKey seed)
        {
            return item => item.AddCapability(new SeedableItemCapability(seed));
        }

        internal static Action<IHasItemCapabilities> Growable(ResourceKey crop)
        {
            return item => item.AddCapability(new GrowableItemCapability(crop));
        }
    }
}