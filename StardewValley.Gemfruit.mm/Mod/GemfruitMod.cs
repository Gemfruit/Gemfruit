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
using Gemfruit.Mod.Placeables;
using Gemfruit.Mod.Resources;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using Module = Gemfruit.Mod.API.Module;

namespace Gemfruit.Mod
{
    public static class GemfruitMod
    {
        public const string GemfruitVersion = "0.1.0";
        
        internal static readonly Logger Logger = new Logger();
        /// <summary>
        /// The Initialization Bus is an <see cref="EventBus"/> used to dispatch
        /// events that relate to the initialization of Gemfruit and mods. This
        /// includes any event where an object is generated before gameplay.
        /// </summary>
        internal static readonly EventBus InitBus = new EventBus();
        
        /// <summary>
        /// The Game Bus is an <see cref="EventBus"/> used to dispatch
        /// events that relate to gameplay.
        /// </summary>
        internal static readonly EventBus GameBus = new EventBus();

        public static ResourceRegistry ResourceRegistry { get; private set; }
        public static PlaceableRegistry PlaceableRegistry { get; private set; }
        public static ItemRegistry ItemRegistry { get; private set; }
        public static MonsterRegistry MonsterRegistry { get; private set; }
        public static MineshaftSpawnRegistry MineshaftSpawnRegistry { get; private set; }
        public static WildernessSpawnRegistry WildernessSpawnRegistry { get; private set; }
        public static ArtifactDropRegistry ArtifactDropRegistry { get; private set; }
        
        private static Dictionary<string, Type> _modList = new Dictionary<string, Type>();
        private static List<string> _modAssetPaths = new List<string>();

        public static void Initialize(Game1 game)
        {
            Console.WriteLine("We at least get here...");
            #if DEBUG
                Logger.MaskLevel = LogLevel.Trace;
            #else
                Logger.MaskLevel = LogLevel.INFO;
            #endif
            Logger.Log(LogLevel.Info, "GemfruitMod", "Loading hooks...");
            HookLoader.LoadHooks();
            Logger.Log(LogLevel.Info, "GemfruitMod", "ResourceRegistry creation");
            ResourceRegistry = new ResourceRegistry();
            Logger.Log(LogLevel.Info, "GemfruitMod", "PlaceableRegistry creation");
            PlaceableRegistry = new PlaceableRegistry(new LocalizedContentManager(game.Content.ServiceProvider, game.Content.RootDirectory));
            Logger.Log(LogLevel.Info, "GemfruitMod", "ItemRegistry creation");
            ItemRegistry = new ItemRegistry(new LocalizedContentManager(game.Content.ServiceProvider, game.Content.RootDirectory));
            Logger.Log(LogLevel.Info, "GemfruitMod", "MonsterRegistry creation");
            MonsterRegistry = new MonsterRegistry();
            Logger.Log(LogLevel.Info, "GemfruitMod", "MineshaftSpawnRegistry creation");
            MineshaftSpawnRegistry = new MineshaftSpawnRegistry();
            Logger.Log(LogLevel.Info, "GemfruitMod", "WildernessSpawnRegistry creation");
            WildernessSpawnRegistry = new WildernessSpawnRegistry();
            Logger.Log(LogLevel.Info, "GemfruitMod", "ArtifactDropRegistry creation");
            ArtifactDropRegistry = new ArtifactDropRegistry();
        }
    
        public static void LoadMods()
        {
            var modPath = Path.Combine(Directory.GetCurrentDirectory(), "Mods");
            var modFolders = Directory.GetDirectories(modPath);

            foreach (var f in modFolders)
            {
                Logger.Log(LogLevel.Debug, "GemfruitMod", $"Looking in '{f}");
                var assemblies = Directory.GetFiles(f).Where(s => s.ToLower().EndsWith(".dll"));
                var subfolders = Directory.GetDirectories(f);

                foreach (var a in assemblies)
                {
                    Logger.Log(LogLevel.Debug, "GemfruitMod", $"Found an assembly '{a}'");
                    var assembly = Assembly.LoadFrom(a);
                    var validClasses = assembly.GetTypes().Where((t, _) => t.GetCustomAttribute<Module>() != null)
                        .ToList();
                    if (!validClasses.Any())
                    {
                        Logger.Log(LogLevel.Debug, "GemfruitMod", "No mods found in assembly - continuing");
                        continue;
                    }

                    foreach (var c in validClasses)
                    {
                        var mod = c.GetCustomAttribute<Module>();
                        _modList.Add(mod.Id, c);
                        Logger.Log(LogLevel.Debug, "GemfruitMod", $"Adding a mod named '{mod.Id}' with class '{c.FullName}'");
                    }
                }
                
                foreach (var sf in subfolders)
                {
                    if (!sf.EndsWith("assets")) continue;
                    _modAssetPaths.Add(sf);
                    Logger.Log(LogLevel.Debug, "GemfruitMod", $"Adding an asset path '{sf}'");
                }
            }

            Logger.Log(LogLevel.Info, "GemfruitMod", $"Loaded {_modList.Count} mods");
        }

        public static void LoadAssets()
        {
            foreach (var f in _modAssetPaths)
            {
                var namespaces = Directory.GetDirectories(f);
                foreach (var n in namespaces)
                {
                    var ns = n.Replace(f + Path.DirectorySeparatorChar, "");
                    var components = Directory.GetDirectories(n);
                    Logger.Log(LogLevel.Debug, "GemfruitMod", $"Found namespace '{ns}'");
                    foreach (var c in components)
                    {
                        var component = c.Replace(n + Path.DirectorySeparatorChar, "");
                        Logger.Log(LogLevel.Debug, "GemfruitMod", $"Found component '{component}'");
                        switch (component)
                        {
                            case "textures":
                            {
                                ResourceRegistry.LoadFromDirectory<Texture2D>(ns, c, c);
                                var subs = Directory.GetDirectories(c);
                                foreach (var s in subs)
                                {
                                    ResourceRegistry.LoadFromDirectory<Texture2D>(ns, c, s);
                                }

                                break;
                            }
                        }
                    }
                }
            }
        }

        public static void LoadGameHooks() => LoadBusHook(GameBus, "GameBus");
        public static void LoadInitHooks() => LoadBusHook(InitBus, "InitBus");

        private static Attribute GetAttributeForName(MethodInfo m, string name)
        {
            switch (name)
            {
                case "InitBus":
                    return m.GetCustomAttribute<InitBusHookAttribute>();
                case "GameBus":
                    return m.GetCustomAttribute<GameBusHookAttribute>();
                default:
                    throw new ArgumentException($"invalid bus type {name}");
            }
        }

        private static void LoadBusHook(EventBus bus, string name)
        {
            foreach (var mod in _modList)
            {
                var methods = mod.Value.GetRuntimeMethods();
                foreach (var m in methods)
                {
                    var attribute = GetAttributeForName(m, name);
                    if (attribute == null) continue;
                    Logger.Log(LogLevel.Debug, "GemfruitMod", $"Found a(n) {name} hook for mod '{mod.Key}' - '{mod.Value.Name}#{m.Name}");
                    var paramList = m.GetParameters();
                    if (paramList.Length != 1)
                    {
                        Logger.Log(LogLevel.Error, "GemfruitMod",
                            $"Unable to load {name} hook for '{mod.Value.Name}#{m.Name}' - wrong number of parameters ({paramList.Length})");
                        continue;
                    }

                    var firstParam = paramList[0].ParameterType;
                    if (!firstParam.IsSubclassOf(typeof(EventBase)))
                    {
                        Logger.Log(LogLevel.Error, "GemfruitMod",
                            $"Unable to load {name} hook for '{mod.Value.Name}#{m.Name} - parameter isn't EventBase!");
                        continue;
                    }
                    
                    // reflection hack to get our event bus to register the event with matching parameters
                    typeof(EventBus)
                        .GetMethod("AddHandler")
                        ?.MakeGenericMethod(firstParam)
                        .Invoke(bus, 
                            new object[]
                            {
                                Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(firstParam), m)
                            });
                }
            }
        }
    }
}