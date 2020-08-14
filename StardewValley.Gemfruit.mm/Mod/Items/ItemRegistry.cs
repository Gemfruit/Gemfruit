using System;
using System.Collections.Generic;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Events;
using Gemfruit.Mod.API.Events.Items;
using Gemfruit.Mod.API.Utility;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Internal.Exceptions;
using Gemfruit.Mod.Items.Capabilities;
using Microsoft.Xna.Framework;
using StardewValley;

namespace Gemfruit.Mod.Items
{
    public class ItemRegistry : PhasableRegistry
    {
        private readonly LocalizedContentManager _content;
        private readonly Dictionary<ResourceKey, Item> _dictionary = new Dictionary<ResourceKey, Item>();
        private static readonly Dictionary<ResourceKey, Action<Item>> ATTACHMENT_ACTIONS = new Dictionary<ResourceKey, Action<Item>>();

        private static Action<Item> VEGETABLE_DEFAULTS = item =>
        {
            item.Capabilities.Add(new PreservableItemCapability(new ResourceKey("pickles")));
            item.Capabilities.Add(new FermentableItemCapability(new ResourceKey("juice")));
        };

        private static Action<Item> SEEDABLE_VEGETABLE(ResourceKey seed)
        {
            return item => { VEGETABLE_DEFAULTS(item); item.Capabilities.Add(new SeedableItemCapability(seed)); };
        }

        private static Action<Item> FRUIT_DEFAULTS = item =>
        {
            item.Capabilities.Add(new PreservableItemCapability(new ResourceKey("jelly")));
            item.Capabilities.Add(new PreservableItemCapability(new ResourceKey("wine")));
        };
        
        private static Action<Item> SEEDABLE_FRUIT(ResourceKey seed)
        {
            return item => { FRUIT_DEFAULTS(item); item.Capabilities.Add(new SeedableItemCapability(seed)); };
        }

        static ItemRegistry()
        {
            // VEGETABLES
            ATTACHMENT_ACTIONS.Add(new ResourceKey("amaranth"), SEEDABLE_VEGETABLE(new ResourceKey("amaranth_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("artichoke"), SEEDABLE_VEGETABLE(new ResourceKey("artichoke_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("beet"), SEEDABLE_VEGETABLE(new ResourceKey("beet_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("bok_choy"), SEEDABLE_VEGETABLE(new ResourceKey("bok_choy_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("cauliflower"), SEEDABLE_VEGETABLE(new ResourceKey("cauliflower_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("corn"), SEEDABLE_VEGETABLE(new ResourceKey("corn_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("eggplant"), SEEDABLE_VEGETABLE(new ResourceKey("eggplant_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("fiddlehead_fern"), VEGETABLE_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new ResourceKey("garlic"), SEEDABLE_VEGETABLE(new ResourceKey("garlic_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("green_bean"), SEEDABLE_VEGETABLE(new ResourceKey("bean_starter")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("hops"), item =>
            {
                item.Capabilities.Add(new PreservableItemCapability(new ResourceKey("pickles")));
                item.Capabilities.Add(new FermentableItemCapability(new ResourceKey("pale_ale")));
                item.Capabilities.Add(new SeedableItemCapability(new ResourceKey("hops_starter")));
            });
            ATTACHMENT_ACTIONS.Add(new ResourceKey("kale"), SEEDABLE_VEGETABLE(new ResourceKey("kale_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("parsnip"), SEEDABLE_VEGETABLE(new ResourceKey("parsnip_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("potato"), SEEDABLE_VEGETABLE(new ResourceKey("potato_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("pumpkin"), SEEDABLE_VEGETABLE(new ResourceKey("pumpkin_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("radish"), SEEDABLE_VEGETABLE(new ResourceKey("radish_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("red_cabbage"), SEEDABLE_VEGETABLE(new ResourceKey("red_cabbage_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("tea_leaves"), item =>
            {
                item.Capabilities.Add(new PreservableItemCapability(new ResourceKey("pickles")));
                item.Capabilities.Add(new FermentableItemCapability(new ResourceKey("green_tea")));
            });
            ATTACHMENT_ACTIONS.Add(new ResourceKey("tomato"), SEEDABLE_VEGETABLE(new ResourceKey("tomato_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("unmilled_rice"), SEEDABLE_VEGETABLE(new ResourceKey("rice_shoot")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("wheat"), item =>
            {
                item.Capabilities.Add(new PreservableItemCapability(new ResourceKey("pickles")));
                item.Capabilities.Add(new FermentableItemCapability(new ResourceKey("beer")));
                item.Capabilities.Add(new SeedableItemCapability(new ResourceKey("wheat_seeds")));
            });
            ATTACHMENT_ACTIONS.Add(new ResourceKey("yam"), SEEDABLE_VEGETABLE(new ResourceKey("yam_seeds")));

            // FRUIT
            ATTACHMENT_ACTIONS.Add(new ResourceKey("ancient_fruit"), SEEDABLE_FRUIT(new ResourceKey("ancient_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("apple"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new ResourceKey("apricot"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new ResourceKey("blackberry"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new ResourceKey("blueberry"), SEEDABLE_FRUIT(new ResourceKey("blueberry_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("cactus_fruit"), SEEDABLE_FRUIT(new ResourceKey("catcus_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("cherry"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new ResourceKey("coconut"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new ResourceKey("cranberries"), SEEDABLE_FRUIT(new ResourceKey("cranberry_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("crystal_fruit"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new ResourceKey("grape"), SEEDABLE_FRUIT(new ResourceKey("grape_starter")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("hot_pepper"), SEEDABLE_FRUIT(new ResourceKey("pepper_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("melon"), SEEDABLE_FRUIT(new ResourceKey("melon_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("orange"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new ResourceKey("peach"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new ResourceKey("pomegranate"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new ResourceKey("rhubarb"), SEEDABLE_FRUIT(new ResourceKey("rhubarb_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("salmonberry"), FRUIT_DEFAULTS);
            ATTACHMENT_ACTIONS.Add(new ResourceKey("spice_berry"), SEEDABLE_FRUIT(new ResourceKey("summer_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("starfruit"), SEEDABLE_FRUIT(new ResourceKey("starfruit_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("strawberry"), SEEDABLE_FRUIT(new ResourceKey("strawberry_seeds")));
            ATTACHMENT_ACTIONS.Add(new ResourceKey("wild_plum"), FRUIT_DEFAULTS);
            
            // OTHER
            ATTACHMENT_ACTIONS.Add(new ResourceKey("wild_horseradish"), item =>
            {
                item.Capabilities.Add(new SeedableItemCapability(new ResourceKey("spring_seeds")));
            });
            ATTACHMENT_ACTIONS.Add(new ResourceKey("common_mushroom"), item =>
            {
                item.Capabilities.Add(new SeedableItemCapability(new ResourceKey("fall_seeds")));
            });
            ATTACHMENT_ACTIONS.Add(new ResourceKey("winter_root"), item =>
            {
                item.Capabilities.Add(new SeedableItemCapability(new ResourceKey("winter_seeds")));
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

            
            GemfruitMod.PlaceableRegistry.RegisterPlaceableItems(this);

            GemfruitMod.InitBus.FireEvent(new ItemRegistrationEvent(this, EventPhase.During));
            GemfruitMod.InitBus.FireEvent(new ItemRegistrationEvent(this, EventPhase.After));
        }

        internal void VanillaRegistration()
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
                    val.AssignSpriteSheetReference(new ResourceKey("Maps\\springobjects"), ItemIDToRectangle(i));
                    val.Key = new ResourceKey(StringUtility.SanitizeName(val.Name));
                    if (_dictionary.ContainsKey(val.Key))
                    {
                        val.Key = new ResourceKey(StringUtility.SanitizeName(val.Name) + "_" + i);
                    }

                    Register(val.Key, val);
                }
            }

            var wdict = _content.Load<Dictionary<int, string>>("Data\\weapons");
            foreach (var i in wdict.Keys)
            {
                var item = Item.ParseWeaponFromString(wdict[i]);
                if (item.IsError())
                {
                    GemfruitMod.Logger.Log(LogLevel.ERROR, "ItemRegistry", item.Error().Message);
                }
                else
                {
                    var val = item.Unwrap();
                    val.AssignSpriteSheetReference(new ResourceKey("TileSheets\\weapons"), WeaponIDToRectangle(i));
                    val.Key = new ResourceKey(StringUtility.SanitizeName(val.Name));
                    if (_dictionary.ContainsKey(val.Key))
                    {
                        val.Key = new ResourceKey(StringUtility.SanitizeName(val.Name) + "_" + i);
                    }
                    Register(val.Key, val);
                }
            }
        }

        public void Register(ResourceKey key, Item item)
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

        public Optional<Item> Get(ResourceKey key)
        {
            return _dictionary.ContainsKey(key) ?
                new Optional<Item>(_dictionary[key]) :
                Optional<Item>.None();
        }
    }
}