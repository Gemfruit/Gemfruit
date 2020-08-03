using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Events;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Items;
using Gemfruit.Mod.Monsters;
using Gemfruit.Mod.Resources;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using StardewValley;
using Module = Gemfruit.Mod.API.Module;

namespace Gemfruit.Mod
{
    public static class GemfruitMod
    {
        public const string GemfruitVersion = "0.1.0";
        
        internal static readonly Logger Logger = new Logger();
        internal static readonly EventBus InitBus = new EventBus();
        internal static readonly EventBus GameBus = new EventBus();

        public static ResourceRegistry ResourceRegistry { get; private set; }
        public static ItemRegistry ItemRegistry { get; private set; }
        public static MonsterRegistry MonsterRegistry { get; private set; }
        public static MineshaftSpawnRegistry MineshaftSpawnRegistry { get; private set; }
        public static WildernessSpawnRegistry WildernessSpawnRegistry { get; private set; }
        
        private static Dictionary<string, Type> _modList = new Dictionary<string, Type>();
        private static List<string> _modAssetPaths = new List<string>();

        public static void Initialize(Game1 game)
        {
            #if DEBUG
                Logger.MaskLevel = LogLevel.TRACE;
            #else
                Logger.MaskLevel = LogLevel.INFO;
            #endif
            HookLoader.LoadHooks();
            ResourceRegistry = new ResourceRegistry();
            MonsterRegistry = new MonsterRegistry();
            MineshaftSpawnRegistry = new MineshaftSpawnRegistry();
            WildernessSpawnRegistry = new WildernessSpawnRegistry();
            ItemRegistry = new ItemRegistry(new LocalizedContentManager(game.Content.ServiceProvider, game.Content.RootDirectory));
        }

        public static void LoadMods()
        {
            var modPath = Path.Combine(Directory.GetCurrentDirectory(), "Mods");
            var modFolders = Directory.GetDirectories(modPath);

            foreach (var f in modFolders)
            {
                Logger.Log(LogLevel.DEBUG, "GemfruitMod", $"Looking in '{f}");
                var assemblies = Directory.GetFiles(f).Where(s => s.ToLower().EndsWith(".dll"));
                var subfolders = Directory.GetDirectories(f);

                foreach (var a in assemblies)
                {
                    Logger.Log(LogLevel.DEBUG, "GemfruitMod", $"Found an assembly '{a}'");
                    var assembly = Assembly.LoadFrom(a);
                    var validClasses = assembly.GetTypes().Where((t, _) => t.GetCustomAttribute<Module>() != null)
                        .ToList();
                    if (!validClasses.Any())
                    {
                        Logger.Log(LogLevel.DEBUG, "GemfruitMod", "No mods found in assembly - continuing");
                        continue;
                    }

                    foreach (var c in validClasses)
                    {
                        var mod = c.GetCustomAttribute<Module>();
                        _modList.Add(mod.Id, c);
                        Logger.Log(LogLevel.DEBUG, "GemfruitMod", $"Adding a mod named '{mod.Id}' with class '{c.FullName}'");
                    }
                }
                
                foreach (var sf in subfolders)
                {
                    if (!sf.EndsWith("assets")) continue;
                    _modAssetPaths.Add(sf);
                    Logger.Log(LogLevel.DEBUG, "GemfruitMod", $"Adding an asset path '{sf}'");
                }
            }

            Logger.Log(LogLevel.INFO, "GemfruitMod", $"Loaded {_modList.Count} mods");
        }

        public static void LoadAssets()
        {
            foreach (var f in _modAssetPaths)
            {
                var namespaces = Directory.GetDirectories(f);
                foreach (var n in namespaces)
                {
                    var namspac = n.Replace(f + Path.DirectorySeparatorChar, "");
                    var components = Directory.GetDirectories(n);
                    Logger.Log(LogLevel.DEBUG, "GemfruitMod", $"Found namespace '{namspac}'");
                    foreach (var c in components)
                    {
                        var component = c.Replace(n + Path.DirectorySeparatorChar, "");
                        Logger.Log(LogLevel.DEBUG, "GemfruitMod", $"Found component '{component}'");
                        switch (component)
                        {
                            case "textures":
                            {
                                ResourceRegistry.LoadFromDirectory<Texture2D>(namspac, c, c);
                                var subs = Directory.GetDirectories(c);
                                foreach (var s in subs)
                                {
                                    ResourceRegistry.LoadFromDirectory<Texture2D>(namspac, c, s);
                                }

                                break;
                            }
                        }
                    }
                }
            }
        }

        public static void LoadInitHooks()
        {
            foreach (var mod in _modList)
            {
                var methods = mod.Value.GetRuntimeMethods();
                foreach (var m in methods)
                {
                    var attribute = m.GetCustomAttribute<InitBusHookAttribute>();
                    if (attribute == null) continue;
                    Logger.Log(LogLevel.DEBUG, "GemfruitMod", $"Found an InitBus hook for mod '{mod.Key}' - '{mod.Value.Name}#{m.Name}");
                    var paramList = m.GetParameters();
                    if (paramList.Length != 1)
                    {
                        Logger.Log(LogLevel.ERROR, "GemfruitMod",
                            $"Unable to load InitBus hook for '{mod.Value.Name}#{m.Name}' - wrong number of parameters ({paramList.Length})");
                        continue;
                    }

                    var firstParam = paramList[0].ParameterType;
                    if (!firstParam.IsSubclassOf(typeof(EventBase)))
                    {
                        Logger.Log(LogLevel.ERROR, "GemfruitMod",
                            $"Unable to load InitBus hook for '{mod.Value.Name}#{m.Name} - parameter isn't EventBase!");
                        continue;
                    }
                    
                    // reflection hack to get our event bus to register the event with matching parameters
                    typeof(EventBus)
                        .GetMethod("AddHandler")
                        ?.MakeGenericMethod(firstParam)
                        .Invoke(InitBus, 
                            new object[]
                            {
                                Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(firstParam), m)
                            });
                }
            }
        }

        public static void LoadGameHooks()
        {
            foreach (var mod in _modList)
            {
                var methods = mod.Value.GetRuntimeMethods();
                foreach (var m in methods)
                {
                    var attribute = m.GetCustomAttribute<GameBusHookAttribute>();
                    if (attribute == null) continue;
                    Logger.Log(LogLevel.DEBUG, "GemfruitMod", $"Found an GameBus hook for mod '{mod.Key}' - '{mod.Value.Name}#{m.Name}");
                    var paramList = m.GetParameters();
                    if (paramList.Length != 1)
                    {
                        Logger.Log(LogLevel.ERROR, "GemfruitMod",
                            $"Unable to load GameBus hook for '{mod.Value.Name}#{m.Name}' - wrong number of parameters ({paramList.Length})");
                        continue;
                    }

                    var firstParam = paramList[0].ParameterType;
                    if (!firstParam.IsSubclassOf(typeof(EventBase)))
                    {
                        Logger.Log(LogLevel.ERROR, "GemfruitMod",
                            $"Unable to load GameBus hook for '{mod.Value.Name}#{m.Name} - parameter isn't EventBase!");
                        continue;
                    }
                    
                    // reflection hack to get our event bus to register the event with matching parameters
                    typeof(EventBus)
                        .GetMethod("AddHandler")
                        ?.MakeGenericMethod(firstParam)
                        .Invoke(GameBus, 
                            new object[]
                            {
                                Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(firstParam), m)
                            });
                }
            }
        }
    }
}