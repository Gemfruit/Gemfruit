using System;
using System.Collections.Generic;
using Gemfruit.Mod.API;
using Gemfruit.Mod.Items;

namespace Gemfruit.Mod.Internal.Transformers
{
    /// <summary>
    /// A static class to apply transformations to incoming new <see cref="ResourceKey"/>s after
    /// the conversion and sanitization process.
    /// </summary>
    /// <remarks>
    /// Some <see cref="ResourceKey"/> will not be correct, as a result of us generating them on the fly from
    /// item names (which can be duplicated). <see cref="ResourceKey"/>s are de-duplicated in a way that doesn't
    /// help us to figure out what the underlying data is, so as a result we're forced to catch some of these
    /// names before they even enter the de-duplication process and transform them to better reflect the item.
    /// </remarks>
    public static class VanillaResourceKeyTransformers
    {
        private delegate ResourceKey ItemKeyTransformer(Item item, int key);

        /// <summary>
        /// A Dictionary of <see cref="ItemKeyTransformer"/>s that maps incoming <see cref="ResourceKey"/>s to
        /// functions that return new <see cref="ResourceKey"/>s.
        /// </summary>
        private static readonly Dictionary<ResourceKey, ItemKeyTransformer> Transformers
            = new Dictionary<ResourceKey, ItemKeyTransformer>();

        static VanillaResourceKeyTransformers()
        {
            Transformers.Add(new ResourceKey("egg"), (_, k) =>
            {
                switch (k)
                {
                    case 176: return new ResourceKey("white_egg");
                    case 180: return new ResourceKey("brown_egg");
                    default: throw new InvalidOperationException($"'{k}' is not a valid egg ID");
                }
            });

            Transformers.Add(new ResourceKey("large_egg"), (_, k) =>
            {
                switch (k)
                {
                    case 174: return new ResourceKey("large_white_egg");
                    case 182: return new ResourceKey("large_brown_egg");
                    default: throw new InvalidOperationException($"'{k}' is not a valid large egg ID");
                }
            });
            
            Transformers.Add(new ResourceKey("strange_doll"), (_, k) =>
            {
                switch (k)
                {
                    case 126: return new ResourceKey("green_strange_doll");
                    case 127: return new ResourceKey("yellow_strange_doll");
                    default: throw new InvalidOperationException($"'{k}' is not a valid strange_doll ID");
                }
            });
            
            Transformers.Add(new ResourceKey("weeds"), (_, k) =>
            {
                switch (k)
                {
                    case 0: return new ResourceKey("unknown_weed_0");
                    case 313: return new ResourceKey("mine_weed_1");
                    case 314: return new ResourceKey("mine_weed_2");
                    case 315: return new ResourceKey("mine_weed_3");
                    case 316: return new ResourceKey("purple_weed_1");
                    case 317: return new ResourceKey("purple_weed_2");
                    case 318: return new ResourceKey("purple_weed_3");
                    case 319: return new ResourceKey("blue_crystal");
                    case 320: return new ResourceKey("purple_crystal");
                    case 321: return new ResourceKey("seafoam_crystal");
                    case 452: return new ResourceKey("unknown_weed_452");
                    case 674: return new ResourceKey("ground_weed_674");
                    case 675: return new ResourceKey("ground_weed_675");
                    case 676: return new ResourceKey("ground_weed_676");
                    case 677: return new ResourceKey("ground_weed_677");
                    case 678: return new ResourceKey("ground_weed_678");
                    case 679: return new ResourceKey("ground_weed_679");
                    case 750: return new ResourceKey("unknown_weed_750");
                    case 784: return new ResourceKey("simple_weed_spring");
                    case 785: return new ResourceKey("simple_weed_summer");
                    case 786: return new ResourceKey("simple_weed_fall");
                    case 792: return new ResourceKey("special_weed_spring");
                    case 793: return new ResourceKey("special_weed_summer");
                    case 794: return new ResourceKey("special_weed_fall");
                    default: throw new InvalidOperationException($"'{k}' is not a valid weed ID");
                }
            });
            
            Transformers.Add(new ResourceKey("stone"), (_, k) =>
            {
                switch (k)
                {
                    case 2: return new ResourceKey("diamond_node");
                    case 4: return new ResourceKey("ruby_node");
                    case 75: return new ResourceKey("geode_node");
                    case 76: return new ResourceKey("frozen_geode_node");
                    case 77: return new ResourceKey("magma_geode_node");
                    case 290: return new ResourceKey("iron_ore_node");
                    case 343: return new ResourceKey("unknown_rock_343");
                    case 390: return new ResourceKey("stone");
                    case 450: return new ResourceKey("unknown_rock_450");
                    case 668: return new ResourceKey("coal_rock_1");
                    case 670: return new ResourceKey("coal_rock_2");
                    case 751: return new ResourceKey("copper_ore_node");
                    case 760: return new ResourceKey("black_rock_1");
                    case 762: return new ResourceKey("black_rock_2");
                    case 764: return new ResourceKey("gold_ore_node");
                    case 765: return new ResourceKey("irdium_node");
                    default: throw new InvalidOperationException($"'{k}' is not a valid stone ID");
                }
            });
        }

        public static ResourceKey ApplyTransformerForKey(Item item, int ikey, ResourceKey key)
        {
            return Transformers.ContainsKey(key) ? Transformers[key].Invoke(item, ikey) : key;
        }
    }
}