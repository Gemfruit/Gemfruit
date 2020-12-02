using System;
using System.Collections.Generic;
using System.Linq;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Events;
using Gemfruit.Mod.API.Events.Placeables;
using Gemfruit.Mod.API.Utility;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Internal.Helpers;
using Gemfruit.Mod.Items;
using StardewValley;

namespace Gemfruit.Mod.Placeables
{
    public class PlaceableRegistry : KeyedRegistry<Placeable>
    {
        private readonly LocalizedContentManager _content;
        private readonly Dictionary<ResourceKey, Placeable> _dictionary = new Dictionary<ResourceKey, Placeable>();
        private static readonly Dictionary<ResourceKey, Action<Placeable>> AttachmentActions = new Dictionary<ResourceKey, Action<Placeable>>();
        private static string _furnitureDefaultDesc;

        public PlaceableRegistry(LocalizedContentManager content)
        {
            _content = content;
        }
        
        protected override void InitializeRecords()
        {
            VanillaRegistrations();
            
            GemfruitMod.InitBus.FireEvent(new PlaceableRegistrationEvent(this, EventPhase.During));
            GemfruitMod.InitBus.FireEvent(new PlaceableRegistrationEvent(this, EventPhase.After));
        }

        private void VanillaRegistrations()
        {
            var fdict = _content.Load<Dictionary<int, string>>("Data\\Furniture");
            _furnitureDefaultDesc = _content.LoadString("Strings\\StringsFromCSFiles:Furniture.cs.12623");
            foreach (var i in fdict.Keys)
            {
                var plac = Placeable.ParseFromFurnitureString(fdict[i], _furnitureDefaultDesc);
                if (plac.IsError())
                {
                    GemfruitMod.Logger.Log(LogLevel.Error, "PlaceableRegistry", plac.UnwrapError().Message);
                }
                else
                {
                    var val = plac.Unwrap();
                    var r = val.Rect;
                    r.Location = VanillaSpritesheetHelper.FurnitureIdToLocation(i);
                    val.AssignSpriteSheetReference(new ResourceKey("TileSheets\\furniture"), r);

                    val.Key = new ResourceKey(StringUtility.SanitizeName(val.Name));
                    if (_dictionary.ContainsKey(val.Key))
                    {
                        val.Key = new ResourceKey(StringUtility.SanitizeName(val.Name) + "_" + i);
                    }

                    Register(val.Key, val);
                }
            }
            
            var cdict = _content.Load<Dictionary<int, string>>("Data\\BigCraftablesInformation");
            foreach (var i in cdict.Keys)
            {
                var plac = Placeable.ParseFromBigCraftableString(cdict[i]);
                if (plac.IsError())
                {
                    GemfruitMod.Logger.Log(LogLevel.Error, "PlaceableRegistry", plac.UnwrapError().Message);
                }
                else
                {
                    var val = plac.Unwrap();
                    var r = val.Rect;
                    r.Location = VanillaSpritesheetHelper.CraftableIdToLocation(i);
                    val.AssignSpriteSheetReference(new ResourceKey("TileSheets\\Craftables"), r);

                    val.Key = new ResourceKey(StringUtility.SanitizeName(val.Name));
                    if (_dictionary.ContainsKey(val.Key))
                    {
                        val.Key = new ResourceKey(StringUtility.SanitizeName(val.Name) + "_" + i);
                    }

                    Register(val.Key, val);
                }
            }
        }

        internal void RegisterPlaceableItems(ItemRegistry r)
        {
            foreach (var fi in _dictionary.Values.Select(f => f.GetPlaceableItem()))
            {
                if (r.Get(fi.Key).IsPresent())
                {
                    fi.Key = new ResourceKey(fi.Key.Namespace, fi.Key.Key + "_placeable");
                }
                r.Register(fi.Key, fi);
            }
        }

        protected override void AddItem(ResourceKey key, Placeable value)
        {
            if (AttachmentActions.ContainsKey(key))
            {
                AttachmentActions[key].Invoke(value);
            }
            
            _dictionary.Add(key, value);
        }

        protected override bool HasKey(ResourceKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public Optional<Placeable> Get(ResourceKey key)
        {
            return _dictionary.ContainsKey(key) ?
                new Optional<Placeable>(_dictionary[key]) :
                Optional<Placeable>.None();
        }
    }
}