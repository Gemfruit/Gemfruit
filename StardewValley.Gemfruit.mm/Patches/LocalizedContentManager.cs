using System;
using System.Collections.Generic;
using System.Globalization;
using Gemfruit.Mod;
using Gemfruit.Mod.API;
using MonoMod;
using StardewValley;

#pragma warning disable 108,114,626,649
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace StardewValley
{
    internal class patch_LocalizedContentManager : LocalizedContentManager
    {
        [MonoModPublic] 
        public readonly Dictionary<string, bool> _localizedAsset;
        
        public patch_LocalizedContentManager(IServiceProvider serviceProvider, string rootDirectory, CultureInfo currentCulture) : base(serviceProvider, rootDirectory, currentCulture)
        {
        }

        public patch_LocalizedContentManager(IServiceProvider serviceProvider, string rootDirectory) : base(serviceProvider, rootDirectory)
        {
        }
        
        public extern T orig_Load<T>(string assetName, LanguageCode language);
        public override T Load<T>(string assetName, LanguageCode language)
        {
            var option = GemfruitMod.ResourceRegistry.Get<T>(new RegistryKey("stardew_valley", assetName));
            T res = option.IsPresent() ? option.Unwrap() : orig_Load<T>(assetName, language);
            return res;
        }
    }
}