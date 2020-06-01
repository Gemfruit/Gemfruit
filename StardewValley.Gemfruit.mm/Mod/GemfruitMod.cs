using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Gemfruit.Mod.API;
using Gemfruit.Mod.Events;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Monsters;
using Module = Gemfruit.Mod.API.Module;

namespace Gemfruit.Mod
{
    public static class GemfruitMod
    {
        public const string GemfruitVersion = "0.1.0";
        
        public static readonly Logger Logger = new Logger();
        internal static readonly EventBus InitBus = new EventBus();
        internal static readonly EventBus GameBus = new EventBus();

        public static MonsterRegistry MonsterRegistry { get; private set; }
        public static MineshaftSpawnRegistry MineshaftSpawnRegistry { get; private set; }
        public static WildernessSpawnRegistry WildernessSpawnRegistry { get; private set; }
        
        private static Dictionary<string, Type> _modList = new Dictionary<string, Type>();

        public static void Initialize()
        {
            #if DEBUG
                Logger.MaskLevel = LogLevel.TRACE;
            #else
                Logger.MaskLevel = LogLevel.INFO;
            #endif
            MonsterRegistry = new MonsterRegistry();
            MineshaftSpawnRegistry = new MineshaftSpawnRegistry();
            WildernessSpawnRegistry = new WildernessSpawnRegistry();
        }

        public static void LoadMods()
        {
            var modPath = Path.Combine(Directory.GetCurrentDirectory(), "Mods");
            var assemblies = Directory.GetFiles(modPath);
            foreach (var a in assemblies)
            {
                if (!a.ToLower().EndsWith(".dll")) continue;
                Logger.Log(LogLevel.DEBUG, "GemfruitMod", $"Found an assembly '{a}'");
                var assembly = Assembly.LoadFrom(a);
                var validClasses = assembly.GetTypes().Where((t, _) => t.GetCustomAttribute<Module>() != null).ToList();
                if (!validClasses.Any())
                {
                    Logger.Log(LogLevel.DEBUG, "GemfruitMod", "No mods found in assembly - continuing");
                    continue;
                }
                foreach (var c in validClasses)
                {
                    var mod = c.GetCustomAttribute<Module>();
                    _modList.Add(mod.Id, c);
                    Console.WriteLine($"Adding a mod named '{mod.Id} with class '{c.FullName}'");
                }
            }
            Logger.Log(LogLevel.INFO, "GemfruitMod", $"Loaded {_modList.Count} mods");
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
                                Delegate.CreateDelegate(typeof(Action<,>).MakeGenericType(firstParam), m)
                            });
                }
            }
        }
    }
}