using System;
using System.Collections.Generic;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Events;
using Gemfruit.Mod.API.Events.Items;
using Gemfruit.Mod.API.Utility;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Internal.Exceptions;
using Microsoft.Xna.Framework;
using StardewValley;

namespace Gemfruit.Mod.Items
{
    public class ItemRegistry : PhasableRegistry
    {
        private readonly LocalizedContentManager _content;
        private readonly Dictionary<RegistryKey, Item> _dictionary = new Dictionary<RegistryKey, Item>();
        private static readonly Dictionary<RegistryKey, Action<Item>> ATTACHMENT_ACTIONS = new Dictionary<RegistryKey, Action<Item>>();

        private static Action<Item> VEGETABLE_DEFAULTS = item =>
        {
            item.Capabilities.Add(new PreservableItemCapability(new RegistryKey("pickles")));
            item.Capabilities.Add(new FermentableItemCapability(new RegistryKey("juice")));
        };

        private static Action<Item> SEEDABLE_VEGETABLE(RegistryKey seed)
        {
            return item => { VEGETABLE_DEFAULTS(item); item.Capabilities.Add(new SeedableItemCapability(seed)); };
        }

        private static Action<Item> FRUIT_DEFAULTS = item =>
        {
            item.Capabilities.Add(new PreservableItemCapability(new RegistryKey("jelly")));
            item.Capabilities.Add(new PreservableItemCapability(new RegistryKey("wine")));
        };
        
        private static Action<Item> SEEDABLE_FRUIT(RegistryKey seed)
        {
            return item => { FRUIT_DEFAULTS(item); item.Capabilities.Add(new SeedableItemCapability(seed)); };
        }

        static ItemRegistry()
        {
            // VEGETABLES
            ATTACHMENT_ACTIONS.Add(new RegistryKey("amaranth"), SEEDABLE_VEGETABLE(new RegistryKey("amaranth_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("artichoke"), SEEDABLE_VEGETABLE(new RegistryKey("artichoke_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("beet"), SEEDABLE_VEGETABLE(new RegistryKey("beet_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("bok_choy"), SEEDABLE_VEGETABLE(new RegistryKey("bok_choy_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("cauliflower"), SEEDABLE_VEGETABLE(new RegistryKey("cauliflower_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("corn"), SEEDABLE_VEGETABLE(new RegistryKey("corn_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("eggplant"), SEEDABLE_VEGETABLE(new RegistryKey("eggplant_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("fiddlehead_fern"), VEGETABLE_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new RegistryKey("garlic"), SEEDABLE_VEGETABLE(new RegistryKey("garlic_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("green_bean"), SEEDABLE_VEGETABLE(new RegistryKey("bean_starter")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("hops"), item =>
            {
                item.Capabilities.Add(new PreservableItemCapability(new RegistryKey("pickles")));
                item.Capabilities.Add(new FermentableItemCapability(new RegistryKey("pale_ale")));
                item.Capabilities.Add(new SeedableItemCapability(new RegistryKey("hops_starter")));
            });
            ATTACHMENT_ACTIONS.Add(new RegistryKey("kale"), SEEDABLE_VEGETABLE(new RegistryKey("kale_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("parsnip"), SEEDABLE_VEGETABLE(new RegistryKey("parsnip_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("potato"), SEEDABLE_VEGETABLE(new RegistryKey("potato_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("pumpkin"), SEEDABLE_VEGETABLE(new RegistryKey("pumpkin_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("radish"), SEEDABLE_VEGETABLE(new RegistryKey("radish_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("red_cabbage"), SEEDABLE_VEGETABLE(new RegistryKey("red_cabbage_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("tea_leaves"), item =>
            {
                item.Capabilities.Add(new PreservableItemCapability(new RegistryKey("pickles")));
                item.Capabilities.Add(new FermentableItemCapability(new RegistryKey("green_tea")));
            });
            ATTACHMENT_ACTIONS.Add(new RegistryKey("tomato"), SEEDABLE_VEGETABLE(new RegistryKey("tomato_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("unmilled_rice"), SEEDABLE_VEGETABLE(new RegistryKey("rice_shoot")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("wheat"), item =>
            {
                item.Capabilities.Add(new PreservableItemCapability(new RegistryKey("pickles")));
                item.Capabilities.Add(new FermentableItemCapability(new RegistryKey("beer")));
                item.Capabilities.Add(new SeedableItemCapability(new RegistryKey("wheat_seeds")));
            });
            ATTACHMENT_ACTIONS.Add(new RegistryKey("yam"), SEEDABLE_VEGETABLE(new RegistryKey("yam_seeds")));

            // FRUIT
            ATTACHMENT_ACTIONS.Add(new RegistryKey("ancient_fruit"), SEEDABLE_FRUIT(new RegistryKey("ancient_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("apple"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new RegistryKey("apricot"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new RegistryKey("blackberry"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new RegistryKey("blueberry"), SEEDABLE_FRUIT(new RegistryKey("blueberry_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("cactus_fruit"), SEEDABLE_FRUIT(new RegistryKey("catcus_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("cherry"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new RegistryKey("coconut"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new RegistryKey("cranberries"), SEEDABLE_FRUIT(new RegistryKey("cranberry_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("crystal_fruit"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new RegistryKey("grape"), SEEDABLE_FRUIT(new RegistryKey("grape_starter")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("hot_pepper"), SEEDABLE_FRUIT(new RegistryKey("pepper_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("melon"), SEEDABLE_FRUIT(new RegistryKey("melon_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("orange"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new RegistryKey("peach"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new RegistryKey("pomegranate"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new RegistryKey("rhubarb"), SEEDABLE_FRUIT(new RegistryKey("rhubarb_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("salmonberry"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new RegistryKey("spice_berry"), SEEDABLE_FRUIT(new RegistryKey("summer_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("starfruit"), SEEDABLE_FRUIT(new RegistryKey("starfruit_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("strawberry"), SEEDABLE_FRUIT(new RegistryKey("strawberry_seeds")));
            ATTACHMENT_ACTIONS.Add(new RegistryKey("wild_plum"), FRUIT_DEFAULTS);
            
            // OTHER
            ATTACHMENT_ACTIONS.Add(new RegistryKey("wild_horseradish"), item =>
            {
                item.Capabilities.Add(new SeedableItemCapability(new RegistryKey("spring_seeds")));
            });
            ATTACHMENT_ACTIONS.Add(new RegistryKey("common_mushroom"), item =>
            {
                item.Capabilities.Add(new SeedableItemCapability(new RegistryKey("fall_seeds")));
            });
            ATTACHMENT_ACTIONS.Add(new RegistryKey("winter_root"), item =>
            {
                item.Capabilities.Add(new SeedableItemCapability(new RegistryKey("winter_seeds")));
            });
        }

        public ItemRegistry(LocalizedContentManager content)
        {
            _content = content;
        }

        internal static Rectangle ItemIDToRectangle(int id)
        {
            var x = id % 24 * 16;
            var y = id / 24 * 16;
            return new Rectangle(x, y, 16, 16);
        }

        internal static Rectangle WeaponIDToRectangle(int id)
        {
            var x = id % 8 * 16;
            var y = id / 8 * 16;
            return new Rectangle(x, y, 16, 16);
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
                    val.AssignSpriteSheetReference(new RegistryKey("Maps\\springobjects"), ItemIDToRectangle(i));
                    var key = new RegistryKey(StringUtility.SanitizeName(val.Name));
                    if (_dictionary.ContainsKey(key))
                    {
                        key = new RegistryKey(StringUtility.SanitizeName(val.Name) + "_" + i);
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
                    val.AssignSpriteSheetReference(new RegistryKey("TileSheets\\weapons"), WeaponIDToRectangle(i));
                    var key = new RegistryKey(StringUtility.SanitizeName(val.Name));
                    if (_dictionary.ContainsKey(key))
                    {
                        key = new RegistryKey(StringUtility.SanitizeName(val.Name) + "_" + i);
                    }
                    Register(key, val);
                }
            }
            
            GemfruitMod.PlaceableRegistry.RegisterPlaceableItems(this);

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
                
                if (ATTACHMENT_ACTIONS.ContainsKey(key))
                {
                    ATTACHMENT_ACTIONS[key].Invoke(item);
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

        public Optional<Item> Get(RegistryKey key)
        {
            return _dictionary.ContainsKey(key) ?
                new Optional<Item>(_dictionary[key]) :
                Optional<Item>.None();
        }
    }
}