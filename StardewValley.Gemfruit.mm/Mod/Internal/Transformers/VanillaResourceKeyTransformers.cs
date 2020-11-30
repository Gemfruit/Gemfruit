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
        private delegate ResourceKey ItemKeyTransformer(Item item);

        /// <summary>
        /// A Dictionary of <see cref="ItemKeyTransformer"/>s that maps incoming <see cref="ResourceKey"/>s to
        /// functions that return new <see cref="ResourceKey"/>s.
        /// </summary>
        private static readonly Dictionary<ResourceKey, ItemKeyTransformer> Transformers
            = new Dictionary<ResourceKey, ItemKeyTransformer>();

        static VanillaResourceKeyTransformers()
        {
            Transformers.Add(new ResourceKey("egg"), i =>
            {
                if (i.Description.Contains("brown"))
                {
                    return new ResourceKey("brown_egg");
                }

                if (i.Description.Contains("white"))
                {
                    return new ResourceKey("white_egg");
                }

                throw new InvalidOperationException($"bad description for egg candidate - '{i.Description}'");
            });

            Transformers.Add(new ResourceKey("large_egg"), i =>
            {
                if (i.Description.Contains("brown"))
                {
                    return new ResourceKey("large_brown_egg");
                }

                if (i.Description.Contains("white"))
                {
                    return new ResourceKey("large_white_egg");
                }

                throw new InvalidOperationException($"bad description for large egg candidate - '{i.Description}'");
            });
        }

        public static ResourceKey ApplyTransformerForKey(Item item, ResourceKey key)
        {
            return Transformers.ContainsKey(key) ? Transformers[key].Invoke(item) : key;
        }
    }
}