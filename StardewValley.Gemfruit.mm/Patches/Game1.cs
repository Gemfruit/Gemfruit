using System;
using Gemfruit.Mod;
using Gemfruit.Mod.Internal;

#pragma warning disable 108,114,626,649
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace StardewValley
{
    internal class patch_Game1 : Game1
    {
        protected extern void orig_Initialize();
        protected override void Initialize()
        {
            Console.WriteLine(  "\n\n"+
                                ".88888.                      .8888b                   oo   dP   \n"+
                                "d8'   `88                     88                            88   \n" +
                                "88        .d8888b. 88d8b.d8b. 88aaa  88d888b. dP    dP dP d8888P \n"+
                                "88   YP88 88ooood8 88'`88'`88 88     88'  `88 88    88 88   88   \n"+
                                "Y8.   .88 88.  ... 88  88  88 88     88       88.  .88 88   88   \n"+
                                " `88888'  `88888P' dP  dP  dP dP     dP       `88888P' dP   dP   \n");
            Console.WriteLine($"Loading Gemfruit v{GemfruitMod.GemfruitVersion}");
            GemfruitMod.Initialize();
            try
            {
                GemfruitMod.LoadMods();
                GemfruitMod.LoadInitHooks();
                GemfruitMod.MineshaftMonsterRegistry.Initialize();
                GemfruitMod.MineshaftSpawnRegistry.Initialize();
                GemfruitMod.WildernessMonsterRegistry.Initialize();
                GemfruitMod.WildernessSpawnRegistry.Initialize();
                GemfruitMod.LoadGameHooks();
            }
            catch (Exception e)
            {
                GemfruitMod.Logger.Log(LogLevel.FATAL, "Game1", "Encountered an exception while initializing GemFruit");
                GemfruitMod.Logger.Log(LogLevel.FATAL, "Game1", e.Message);
                GemfruitMod.Logger.Log(LogLevel.FATAL, "Game1", e.StackTrace);
                Environment.Exit(-1);
            }

            orig_Initialize();
        }
    }
}