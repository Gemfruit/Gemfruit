using System;
using System.Collections.Generic;
using System.Linq;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Events;
using Gemfruit.Mod.API.Events.Items;
using Gemfruit.Mod.API.Utility;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Internal.Exceptions;
using Gemfruit.Mod.Items;
using Microsoft.Xna.Framework;
using StardewValley;

namespace Gemfruit.Mod.Placeables
{
    public class PlaceableRegistry : PhasableRegistry
    {
        private readonly LocalizedContentManager _content;
        private readonly Dictionary<RegistryKey, Placeable> _dictionary = new Dictionary<RegistryKey, Placeable>();
        private static readonly Dictionary<RegistryKey, Action<Placeable>> ATTACHMENT_ACTIONS = new Dictionary<RegistryKey, Action<Placeable>>();
        private static string _furnitureDefaultDesc;
        
        internal static Point FurnitureIDToLocation(int id)
        {
            var x = id % 32 * 16;
            var y = id / 32 * 16;
            return new Point(x, y);
        }
        public PlaceableRegistry(LocalizedContentManager content)
        {
            _content = content;
        }
        
        protected override void InitializeRecords()
        {
            var fdict = _content.Load<Dictionary<int, string>>("Data\\Furniture");
            _furnitureDefaultDesc = _content.LoadString("Strings\\StringsFromCSFiles:Furniture.cs.12623");
            foreach (var i in fdict.Keys)
            {
                var plac = Placeable.ParseFromFurnitureString(fdict[i], _furnitureDefaultDesc);
                if (plac.IsError())
                {
                    GemfruitMod.Logger.Log(LogLevel.ERROR, "PlaceableRegistry", plac.Error().Message);
                }
                else
                {
                    var val = plac.Unwrap();
                    var r = val.Rect;
                    r.Location = FurnitureIDToLocation(i);
                    val.AssignSpriteSheetReference(new RegistryKey("TileSheets\\furniture"), r);

                    var key = new RegistryKey(StringUtility.SanitizeName(val.Name));
                    if (_dictionary.ContainsKey(key))
                    {
                        key = new RegistryKey(StringUtility.SanitizeName(val.Name) + "_" + i);
                    }

                    Register(key, val);
                }
            }
            
            GemfruitMod.InitBus.FireEvent(new PlaceableRegistrationEvent(this, EventPhase.During));
            GemfruitMod.InitBus.FireEvent(new PlaceableRegistrationEvent(this, EventPhase.After));
        }

        internal void RegisterPlaceableItems(ItemRegistry r)
        {
            foreach (var fi in _dictionary.Values.Select(f => f.GetPlaceableItem()))
            {
                r.Register(fi.Key, fi);
            }
        }
        
        public void Register(RegistryKey key, Placeable plac)
        {
            if (CurrentPhase == RegistryPhase.Open)
            {
                if (_dictionary.ContainsKey(key))
                {
                    throw new RegistryConflictException(key);
                }
                
                if (ATTACHMENT_ACTIONS.ContainsKey(key))
                {
                    ATTACHMENT_ACTIONS[key].Invoke(plac);
                }

                GemfruitMod.Logger.Log(LogLevel.DEBUG, GetType().Name,
                    $"Registering placeable '{key}'");
                _dictionary.Add(key, plac);
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.ERROR, GetType().Name,
                    $"Attempted to register '{key}' before corresponding lifecycle event!");
            }
        }

        public Optional<Placeable> Get(RegistryKey key)
        {
            return _dictionary.ContainsKey(key) ?
                new Optional<Placeable>(_dictionary[key]) :
                Optional<Placeable>.None();
        }
    }
}