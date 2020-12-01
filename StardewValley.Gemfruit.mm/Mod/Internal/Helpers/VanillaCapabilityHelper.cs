using System;
using Gemfruit.Mod.API;
using Gemfruit.Mod.Items;
using Gemfruit.Mod.Items.Capabilities;

namespace Gemfruit.Mod.Internal.Helpers
{
    internal static class VanillaCapabilityHelper
    {
        internal static readonly Action<IHasItemCapabilities> DefaultVegetable = item =>
        {
            item.AddCapability(new PreservableItemCapability(new ResourceKey("pickles"), 4000));
            item.AddCapability(new FermentableItemCapability(new ResourceKey("juice"), 6000));
        };

        internal static Action<IHasItemCapabilities> SeedableVegetable(ResourceKey seed)
        {
            return item =>
            {
                DefaultVegetable(item);
                item.AddCapability(new SeedableItemCapability(seed));
            };
        }

        internal static readonly Action<IHasItemCapabilities> DefaultFruit = item =>
        {
            item.AddCapability(new PreservableItemCapability(new ResourceKey("jelly"), 4000));
            item.AddCapability(new FermentableItemCapability(new ResourceKey("wine"), 10000));
        };

        internal static Action<IHasItemCapabilities> SeedableFruit(ResourceKey seed)
        {
            return item =>
            {
                DefaultFruit(item);
                item.AddCapability(new SeedableItemCapability(seed));
            };
        }

        internal static readonly Action<IHasItemCapabilities> DefaultCrystal = item =>
        {
            item.AddCapability(
                new CrystalariumizableItemCapability(CrystalariumizableItemCapability.DefaultCrystalTime));
        };

        internal static Action<IHasItemCapabilities> TimedCrystal(int minutes)
        {
            return item => { item.AddCapability(new CrystalariumizableItemCapability(minutes)); };
        }

        internal static Action<IHasItemCapabilities> Seed(ResourceKey crop)
        {
            return item => item.AddCapability(new GrowableItemCapability(crop));
        }

        internal static Action<IHasItemCapabilities> Mayonnaise(ResourceKey mayo)
        {
            return item => item.AddCapability(new MayonnaiseableItemCapability(mayo));
        }

        internal static Action<IHasItemCapabilities> QualityMayonnaise(ResourceKey mayo, ItemQuality quality)
        {
            return item => item.AddCapability(new MayonnaiseableItemCapability(mayo, quality: quality));
        }
    }
}