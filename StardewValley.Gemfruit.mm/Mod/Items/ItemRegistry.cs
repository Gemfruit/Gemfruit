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

        private static readonly Dictionary<ResourceKey, Action<Item>> AttachmentActions =
            new Dictionary<ResourceKey, Action<Item>>();
        
        private static Rectangle ItemIdToRectangle(int id)
        {
            var x = id % 24 * 16;
            var y = id / 24 * 16;
            return new Rectangle(x, y, 16, 16);
        }

        private static Rectangle WeaponIdToRectangle(int id)
        {
            var x = id % 8 * 16;
            var y = id / 8 * 16;
            return new Rectangle(x, y, 16, 16);
        }
        
        private static readonly Action<Item> DefaultVegetable = item =>
        {
            item.Capabilities.Add(new PreservableItemCapability(new ResourceKey("pickles")));
            item.Capabilities.Add(new FermentableItemCapability(new ResourceKey("juice")));
        };

        private static Action<Item> SeedableVegetable(ResourceKey seed)
        {
            return item =>
            {
                DefaultVegetable(item);
                item.Capabilities.Add(new SeedableItemCapability(seed));
            };
        }

        private static readonly Action<Item> DefaultFruit = item =>
        {
            item.Capabilities.Add(new PreservableItemCapability(new ResourceKey("jelly")));
            item.Capabilities.Add(new PreservableItemCapability(new ResourceKey("wine")));
        };

        private static Action<Item> SeedableFruit(ResourceKey seed)
        {
            return item =>
            {
                DefaultFruit(item);
                item.Capabilities.Add(new SeedableItemCapability(seed));
            };
        }

        private static readonly Action<Item> DefaultCrystal = item =>
        {
            item.Capabilities.Add(
                new CrystalariumizableItemCapability(CrystalariumizableItemCapability.DefaultCrystalTime));
        };

        private static Action<Item> TimedCrystal(int minutes)
        {
            return item => { item.Capabilities.Add(new CrystalariumizableItemCapability(minutes)); };
        }

        static ItemRegistry()
        {
            // VEGETABLES
            AttachmentActions.Add(new ResourceKey("amaranth"), SeedableVegetable(new ResourceKey("amaranth_seeds")));
            AttachmentActions.Add(new ResourceKey("artichoke"),
                SeedableVegetable(new ResourceKey("artichoke_seeds")));
            AttachmentActions.Add(new ResourceKey("beet"), SeedableVegetable(new ResourceKey("beet_seeds")));
            AttachmentActions.Add(new ResourceKey("bok_choy"), SeedableVegetable(new ResourceKey("bok_choy_seeds")));
            AttachmentActions.Add(new ResourceKey("cauliflower"),
                SeedableVegetable(new ResourceKey("cauliflower_seeds")));
            AttachmentActions.Add(new ResourceKey("corn"), SeedableVegetable(new ResourceKey("corn_seeds")));
            AttachmentActions.Add(new ResourceKey("eggplant"), SeedableVegetable(new ResourceKey("eggplant_seeds")));
            AttachmentActions.Add(new ResourceKey("fiddlehead_fern"), DefaultVegetable);
            AttachmentActions.Add(new ResourceKey("garlic"), SeedableVegetable(new ResourceKey("garlic_seeds")));
            AttachmentActions.Add(new ResourceKey("green_bean"), SeedableVegetable(new ResourceKey("bean_starter")));
            AttachmentActions.Add(new ResourceKey("hops"), item =>
            {
                item.Capabilities.Add(new PreservableItemCapability(new ResourceKey("pickles")));
                item.Capabilities.Add(new FermentableItemCapability(new ResourceKey("pale_ale")));
                item.Capabilities.Add(new SeedableItemCapability(new ResourceKey("hops_starter")));
            });
            AttachmentActions.Add(new ResourceKey("kale"), SeedableVegetable(new ResourceKey("kale_seeds")));
            AttachmentActions.Add(new ResourceKey("parsnip"), SeedableVegetable(new ResourceKey("parsnip_seeds")));
            AttachmentActions.Add(new ResourceKey("potato"), SeedableVegetable(new ResourceKey("potato_seeds")));
            AttachmentActions.Add(new ResourceKey("pumpkin"), SeedableVegetable(new ResourceKey("pumpkin_seeds")));
            AttachmentActions.Add(new ResourceKey("radish"), SeedableVegetable(new ResourceKey("radish_seeds")));
            AttachmentActions.Add(new ResourceKey("red_cabbage"),
                SeedableVegetable(new ResourceKey("red_cabbage_seeds")));
            AttachmentActions.Add(new ResourceKey("tea_leaves"), item =>
            {
                item.Capabilities.Add(new PreservableItemCapability(new ResourceKey("pickles")));
                item.Capabilities.Add(new FermentableItemCapability(new ResourceKey("green_tea")));
            });
            AttachmentActions.Add(new ResourceKey("tomato"), SeedableVegetable(new ResourceKey("tomato_seeds")));
            AttachmentActions.Add(new ResourceKey("unmilled_rice"), SeedableVegetable(new ResourceKey("rice_shoot")));
            AttachmentActions.Add(new ResourceKey("wheat"), item =>
            {
                item.Capabilities.Add(new PreservableItemCapability(new ResourceKey("pickles")));
                item.Capabilities.Add(new FermentableItemCapability(new ResourceKey("beer")));
                item.Capabilities.Add(new SeedableItemCapability(new ResourceKey("wheat_seeds")));
            });
            AttachmentActions.Add(new ResourceKey("yam"), SeedableVegetable(new ResourceKey("yam_seeds")));

            // FRUIT
            AttachmentActions.Add(new ResourceKey("ancient_fruit"), SeedableFruit(new ResourceKey("ancient_seeds")));
            AttachmentActions.Add(new ResourceKey("apple"), DefaultFruit);
            AttachmentActions.Add(new ResourceKey("apricot"), DefaultFruit);
            AttachmentActions.Add(new ResourceKey("blackberry"), DefaultFruit);
            AttachmentActions.Add(new ResourceKey("blueberry"), SeedableFruit(new ResourceKey("blueberry_seeds")));
            AttachmentActions.Add(new ResourceKey("cactus_fruit"), SeedableFruit(new ResourceKey("catcus_seeds")));
            AttachmentActions.Add(new ResourceKey("cherry"), DefaultFruit);
            AttachmentActions.Add(new ResourceKey("coconut"), DefaultFruit);
            AttachmentActions.Add(new ResourceKey("cranberries"), SeedableFruit(new ResourceKey("cranberry_seeds")));
            AttachmentActions.Add(new ResourceKey("crystal_fruit"), DefaultFruit);
            AttachmentActions.Add(new ResourceKey("grape"), SeedableFruit(new ResourceKey("grape_starter")));
            AttachmentActions.Add(new ResourceKey("hot_pepper"), SeedableFruit(new ResourceKey("pepper_seeds")));
            AttachmentActions.Add(new ResourceKey("melon"), SeedableFruit(new ResourceKey("melon_seeds")));
            AttachmentActions.Add(new ResourceKey("orange"), DefaultFruit);
            AttachmentActions.Add(new ResourceKey("peach"), DefaultFruit);
            AttachmentActions.Add(new ResourceKey("pomegranate"), DefaultFruit);
            AttachmentActions.Add(new ResourceKey("rhubarb"), SeedableFruit(new ResourceKey("rhubarb_seeds")));
            AttachmentActions.Add(new ResourceKey("salmonberry"), DefaultFruit);
            AttachmentActions.Add(new ResourceKey("spice_berry"), SeedableFruit(new ResourceKey("summer_seeds")));
            AttachmentActions.Add(new ResourceKey("starfruit"), SeedableFruit(new ResourceKey("starfruit_seeds")));
            AttachmentActions.Add(new ResourceKey("strawberry"), SeedableFruit(new ResourceKey("strawberry_seeds")));
            AttachmentActions.Add(new ResourceKey("wild_plum"), DefaultFruit);

            // OTHER SEEDS
            AttachmentActions.Add(new ResourceKey("wild_horseradish"),
                item => { item.Capabilities.Add(new SeedableItemCapability(new ResourceKey("spring_seeds"))); });
            AttachmentActions.Add(new ResourceKey("common_mushroom"),
                item => { item.Capabilities.Add(new SeedableItemCapability(new ResourceKey("fall_seeds"))); });
            AttachmentActions.Add(new ResourceKey("winter_root"),
                item => { item.Capabilities.Add(new SeedableItemCapability(new ResourceKey("winter_seeds"))); });

            // CRYSTAL
            AttachmentActions.Add(new ResourceKey("emerald"), TimedCrystal(3000));
            AttachmentActions.Add(new ResourceKey("aquamarine"), TimedCrystal(2240));
            AttachmentActions.Add(new ResourceKey("ruby"), TimedCrystal(3000));
            AttachmentActions.Add(new ResourceKey("amethyst"), TimedCrystal(1360));
            AttachmentActions.Add(new ResourceKey("topaz"), TimedCrystal(1120));
            AttachmentActions.Add(new ResourceKey("jade"), TimedCrystal(2400));
            AttachmentActions.Add(new ResourceKey("diamond"), TimedCrystal(7200));
            AttachmentActions.Add(new ResourceKey("quartz"), TimedCrystal(420));
            AttachmentActions.Add(new ResourceKey("fire_quartz"), TimedCrystal(1300));
            AttachmentActions.Add(new ResourceKey("frozen_tear"), TimedCrystal(1120));
            AttachmentActions.Add(new ResourceKey("earth_crystal"), TimedCrystal(800));
            AttachmentActions.Add(new ResourceKey("tigerseye"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("opal"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("fire_opal"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("alamite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("bixite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("baryte"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("aerinite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("calcite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("dolomite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("esperite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("fluorapatite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("geminite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("helvite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("jamborite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("jagoite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("kyanite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("lunarite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("malachite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("neptunite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("lemon_stone"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("nekoite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("orpiment"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("petrified_slime"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("thunder_egg"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("pyrite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("ocean_stone"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("ghost_crystal"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("jasper"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("celestine"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("marble"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("sandstone"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("granite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("basalt"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("limestone"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("soapstone"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("hemaite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("mudstone"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("obsidian"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("slate"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("fairy_stone"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("star_shards"), DefaultCrystal);
        }

        public ItemRegistry(LocalizedContentManager content)
        {
            _content = content;
        }
        
        protected override void InitializeRecords()
        {
            VanillaRegistration();
            GemfruitMod.PlaceableRegistry.RegisterPlaceableItems(this);

            GemfruitMod.InitBus.FireEvent(new ItemRegistrationEvent(this, EventPhase.During));
            GemfruitMod.InitBus.FireEvent(new ItemRegistrationEvent(this, EventPhase.After));
        }

        private void VanillaRegistration()
        {
            var dict = _content.Load<Dictionary<int, string>>("Data\\ObjectInformation");
            foreach (var i in dict.Keys)
            {
                var item = Item.ParseFromString(dict[i]);
                if (item.IsError())
                {
                    GemfruitMod.Logger.Log(LogLevel.Error, "ItemRegistry", item.Error().Message);
                }
                else
                {
                    var val = item.Unwrap();
                    val.AssignSpriteSheetReference(new ResourceKey("Maps\\springobjects"), ItemIdToRectangle(i));
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
                    GemfruitMod.Logger.Log(LogLevel.Error, "ItemRegistry", item.Error().Message);
                }
                else
                {
                    var val = item.Unwrap();
                    val.AssignSpriteSheetReference(new ResourceKey("TileSheets\\weapons"), WeaponIdToRectangle(i));
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

                if (AttachmentActions.ContainsKey(key))
                {
                    AttachmentActions[key].Invoke(item);
                }

                GemfruitMod.Logger.Log(LogLevel.Debug, GetType().Name,
                    $"Registering item '{key}'");
                _dictionary.Add(key, item);
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.Error, GetType().Name,
                    $"Attempted to register '{key}' before corresponding lifecycle event!");
            }
        }

        public Optional<Item> Get(ResourceKey key)
        {
            return _dictionary.ContainsKey(key) ? new Optional<Item>(_dictionary[key]) : Optional<Item>.None();
        }
    }
}