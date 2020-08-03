using System;
using System.Collections.Generic;
using System.Linq;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Events;
using Gemfruit.Mod.API.Events.Items;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Internal.Exceptions;
using StardewValley;

namespace Gemfruit.Mod.Items
{
    public class ItemRegistry : PhasableRegistry
    {
        private readonly LocalizedContentManager _content;
        private readonly Dictionary<RegistryKey, Item> _dictionary = new Dictionary<RegistryKey, Item>();

        public ItemRegistry(LocalizedContentManager content)
        {
            _content = content;
        }

        private static string SanitizeName(string name)
        {
            return name.ToLower().Replace(' ', '_')
                .Replace('-', '_')
                .Replace("l.", "large")
                .Replace(":", "")
                .Replace("'", "");
        }
        
        protected override void InitializeRecords()
        {
            var dict = _content.Load<Dictionary<int, string>>("Data\\ObjectInformation");
            foreach (var i in dict.Keys)
            {
                var item = Item.ParseFromString(dict[i]);
                if (item.IsError())
                {
                    GemfruitMod.Logger.Log(LogLevel.ERROR, "ItemRegistry", item.Error().Message);
                }
                else
                {
                    var val = item.Unwrap();
                    var key = new RegistryKey(SanitizeName(val.Name));
                    if (_dictionary.ContainsKey(key))
                    {
                        key = new RegistryKey(SanitizeName(val.Name) + "_" + i);
                    }
                    Register(key, val);
                }
            }

            var wdict = _content.Load<Dictionary<int, string>>("Data\\weapons");
            foreach (var i in wdict.Keys)
            {
                var item = WeaponItem.ParseFromString(wdict[i]);
                if (item.IsError())
                {
                    GemfruitMod.Logger.Log(LogLevel.ERROR, "ItemRegistry", item.Error().Message);
                }
                else
                {
                    var val = item.Unwrap();
                    var key = new RegistryKey(SanitizeName(val.Name));
                    if (_dictionary.ContainsKey(key))
                    {
                        key = new RegistryKey(SanitizeName(val.Name) + "_" + i);
                    }
                    Register(key, val);
                }
            }
            GemfruitMod.InitBus.FireEvent(new ItemRegistrationEvent(this, EventPhase.During));
            GemfruitMod.InitBus.FireEvent(new ItemRegistrationEvent(this, EventPhase.After));
        }

        public void Register(RegistryKey key, Item item)
        {
            if (CurrentPhase == RegistryPhase.Open)
            {
                if (_dictionary.ContainsKey(key))
                {
                    throw new RegistryConflictException(key);
                }

                GemfruitMod.Logger.Log(LogLevel.DEBUG, GetType().Name,
                    $"Registering item '{key}'");
                _dictionary.Add(key, item);
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.ERROR, GetType().Name,
                    $"Attempted to register '{key}' before corresponding lifecycle event!");
            }
        }
    }
}