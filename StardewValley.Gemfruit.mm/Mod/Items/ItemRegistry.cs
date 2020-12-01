using System;
using System.Collections.Generic;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Events;
using Gemfruit.Mod.API.Events.Items;
using Gemfruit.Mod.API.Exceptions;
using Gemfruit.Mod.API.Utility;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Internal.Helpers;
using Gemfruit.Mod.Internal.Transformers;
using Gemfruit.Mod.Items.Capabilities;
using StardewValley;

using static Gemfruit.Mod.Internal.Helpers.VanillaCapabilityHelper;

namespace Gemfruit.Mod.Items
{
    public class ItemRegistry : PhasableRegistry
    {
        private readonly LocalizedContentManager _content;
        private readonly Dictionary<ResourceKey, Item> _dictionary = new Dictionary<ResourceKey, Item>();

        private static readonly Dictionary<ResourceKey, Action<Item>> AttachmentActions =
            new Dictionary<ResourceKey, Action<Item>>();

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
                item.AddCapability(new PreservableItemCapability(new ResourceKey("pickles"), 4000));
                item.AddCapability(new FermentableItemCapability(new ResourceKey("pale_ale"), 2250));
                item.AddCapability(new SeedableItemCapability(new ResourceKey("hops_starter")));
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
                item.AddCapability(new PreservableItemCapability(new ResourceKey("pickles"), 4000));
                item.AddCapability(new FermentableItemCapability(new ResourceKey("green_tea"), 180));
            });
            AttachmentActions.Add(new ResourceKey("tomato"), SeedableVegetable(new ResourceKey("tomato_seeds")));
            AttachmentActions.Add(new ResourceKey("unmilled_rice"), SeedableVegetable(new ResourceKey("rice_shoot")));
            AttachmentActions.Add(new ResourceKey("wheat"), item =>
            {
                item.AddCapability(new PreservableItemCapability(new ResourceKey("pickles"), 4000));
                item.AddCapability(new FermentableItemCapability(new ResourceKey("beer"), 1750));
                item.AddCapability(new SeedableItemCapability(new ResourceKey("wheat_seeds")));
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
                item => { item.AddCapability(new SeedableItemCapability(new ResourceKey("spring_seeds"))); });
            AttachmentActions.Add(new ResourceKey("common_mushroom"),
                item => { item.AddCapability(new SeedableItemCapability(new ResourceKey("fall_seeds"))); });
            AttachmentActions.Add(new ResourceKey("winter_root"),
                item => { item.AddCapability(new SeedableItemCapability(new ResourceKey("winter_seeds"))); });

            // SEEDS
            AttachmentActions.Add(new ResourceKey("rice_shoot"), Seed(new ResourceKey("unmilled_rice")));
            AttachmentActions.Add(new ResourceKey("amaranth_seeds"), Seed(new ResourceKey("amaranth")));
            AttachmentActions.Add(new ResourceKey("grape_starter"), Seed(new ResourceKey("grape")));
            AttachmentActions.Add(new ResourceKey("hops_starter"), Seed(new ResourceKey("hops")));
            AttachmentActions.Add(new ResourceKey("rare_seed"), Seed(new ResourceKey("gem_berry")));
            AttachmentActions.Add(new ResourceKey("fairy_seeds"), Seed(new ResourceKey("fairy_rose")));
            AttachmentActions.Add(new ResourceKey("tulip_bulb"), Seed(new ResourceKey("tulip")));
            AttachmentActions.Add(new ResourceKey("jazz_seeds"), Seed(new ResourceKey("blue_jazz")));
            AttachmentActions.Add(new ResourceKey("sunflower_seeds"), Seed(new ResourceKey("sunflower")));
            AttachmentActions.Add(new ResourceKey("coffee_bean"), item =>
            {
                item.AddCapability(new FermentableItemCapability(new ResourceKey("coffee"), 120, 5));
                item.AddCapability(new GrowableItemCapability(new ResourceKey("coffee")));
            });
            AttachmentActions.Add(new ResourceKey("poppy_seeds"), Seed(new ResourceKey("poppy")));
            AttachmentActions.Add(new ResourceKey("spangle_seeds"), Seed(new ResourceKey("summer_spangle")));
            AttachmentActions.Add(new ResourceKey("parsnip_seeds"), Seed(new ResourceKey("parsnip")));
            AttachmentActions.Add(new ResourceKey("bean_starter"), Seed(new ResourceKey("green_bean")));
            AttachmentActions.Add(new ResourceKey("cauliflower_seeds"), Seed(new ResourceKey("cauliflower")));
            AttachmentActions.Add(new ResourceKey("potato_seeds"), Seed(new ResourceKey("potato")));
            AttachmentActions.Add(new ResourceKey("garlic_seeds"), Seed(new ResourceKey("garlic")));
            AttachmentActions.Add(new ResourceKey("kale_seeds"), Seed(new ResourceKey("kale")));
            AttachmentActions.Add(new ResourceKey("rhubarb_seeds"), Seed(new ResourceKey("rhubarb")));
            AttachmentActions.Add(new ResourceKey("melon_seeds"), Seed(new ResourceKey("melon")));
            AttachmentActions.Add(new ResourceKey("tomato_seeds"), Seed(new ResourceKey("tomato")));
            AttachmentActions.Add(new ResourceKey("blueberry_seeds"), Seed(new ResourceKey("blueberry")));
            AttachmentActions.Add(new ResourceKey("pepper_seeds"), Seed(new ResourceKey("hot_pepper")));
            AttachmentActions.Add(new ResourceKey("wheat_seeds"), Seed(new ResourceKey("wheat")));
            AttachmentActions.Add(new ResourceKey("radish_seeds"), Seed(new ResourceKey("radish")));
            AttachmentActions.Add(new ResourceKey("red_cabbage_seeds"), Seed(new ResourceKey("red_cabbage")));
            AttachmentActions.Add(new ResourceKey("starfruit_seeds"), Seed(new ResourceKey("starfruit")));
            AttachmentActions.Add(new ResourceKey("corn_seeds"), Seed(new ResourceKey("corn")));
            AttachmentActions.Add(new ResourceKey("eggplant_seeds"), Seed(new ResourceKey("eggplant")));
            AttachmentActions.Add(new ResourceKey("artichoke_seeds"), Seed(new ResourceKey("artichoke")));
            AttachmentActions.Add(new ResourceKey("pumpkin_seeds"), Seed(new ResourceKey("pumpkin")));
            AttachmentActions.Add(new ResourceKey("bok_choy_seeds"), Seed(new ResourceKey("bok_choy")));
            AttachmentActions.Add(new ResourceKey("yam_seeds"), Seed(new ResourceKey("yam")));
            AttachmentActions.Add(new ResourceKey("cranberry_seeds"), Seed(new ResourceKey("cranberries")));
            AttachmentActions.Add(new ResourceKey("beet_seeds"), Seed(new ResourceKey("beet")));
            AttachmentActions.Add(new ResourceKey("spring_seeds"), Seed(new ResourceKey("spring_seeds")));
            AttachmentActions.Add(new ResourceKey("summer_seeds"), Seed(new ResourceKey("summer_seeds")));
            AttachmentActions.Add(new ResourceKey("fall_seeds"), Seed(new ResourceKey("fall_seeds")));
            AttachmentActions.Add(new ResourceKey("winter_seeds"), Seed(new ResourceKey("winter_seeds")));
            AttachmentActions.Add(new ResourceKey("ancient_seeds"), Seed(new ResourceKey("ancient_fruit")));
            AttachmentActions.Add(new ResourceKey("strawberry_seeds"), Seed(new ResourceKey("strawberry")));
            AttachmentActions.Add(new ResourceKey("cactus_seeds"), Seed(new ResourceKey("cactus_fruit")));

            // OTHER PRESERVABLE
            AttachmentActions.Add(new ResourceKey("roe"),
                item => { item.AddCapability(new RoePreservableItemCapability()); });

            // MAYO
            AttachmentActions.Add(new ResourceKey("dinosaur_egg"),
                Mayonnaise(new ResourceKey("dinosaur_mayonnaise")));
            AttachmentActions.Add(new ResourceKey("white_egg"), 
                Mayonnaise(new ResourceKey("mayonnaise")));
            AttachmentActions.Add(new ResourceKey("brown_egg"), 
                Mayonnaise(new ResourceKey("mayonnaise")));
            AttachmentActions.Add(new ResourceKey("large_white_egg"), 
                QualityMayonnaise(new ResourceKey("mayonnaise"), ItemQuality.Gold));
            AttachmentActions.Add(new ResourceKey("large_brown_egg"), 
                QualityMayonnaise(new ResourceKey("mayonnaise"), ItemQuality.Gold));
            AttachmentActions.Add(new ResourceKey("void_egg"), 
                Mayonnaise(new ResourceKey("void_mayonnaise")));
            AttachmentActions.Add(new ResourceKey("duck_egg"), 
                Mayonnaise(new ResourceKey("duck_mayonnaise")));
            
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
            AttachmentActions.Add(new ResourceKey("hematite"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("mudstone"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("obsidian"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("slate"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("fairy_stone"), DefaultCrystal);
            AttachmentActions.Add(new ResourceKey("star_shards"), DefaultCrystal);
        }

        internal ItemRegistry(LocalizedContentManager content)
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

        /// <summary>
        /// Private internal function for parsing vanilla data from the game's content files.
        /// </summary>
        /// <remarks>
        /// This is one of the hackiest methods in the entire project - it's meant to, ostenisbly, parse vanilla data.
        /// Since we're trying to fit as much data as we can with has little outside intervention as possible,
        /// we're forced to jump through a few hoops to interpret the data in a way that meshes with our
        /// system in a sensible way.
        /// </remarks>
        private void VanillaRegistration()
        {
            var objectInfo = _content.Load<Dictionary<int, string>>("Data\\ObjectInformation");
            foreach (var i in objectInfo.Keys)
            {
                var item = Item.ParseFromString(objectInfo[i]);
                if (item.IsError())
                {
                    GemfruitMod.Logger.Log(LogLevel.Error, "ItemRegistry", item.UnwrapError().Message);
                }
                else
                {
                    var val = item.Unwrap();
                    val.AssignSpriteSheetReference(new ResourceKey("Maps\\springobjects"), VanillaSpritesheetHelper.ItemIdToRectangle(i));
                    val.Key = VanillaResourceKeyTransformers.ApplyTransformerForKey(val, i, new ResourceKey(StringUtility.SanitizeName(val.Name)));
                    if (_dictionary.ContainsKey(val.Key))
                    {
                        val.Key = new ResourceKey(StringUtility.SanitizeName(val.Name) + "_" + i);
                    }
                    
                    // If the item is presumably an artifact we're forced to perform some extra parsing on it.
                    if (val.Type.Contains("Arch"))
                    {
                        GemfruitMod.ArtifactDropRegistry.AddVanillaItem(val.Key, objectInfo[i]);
                    }

                    Register(val.Key, val);
                }
            }

            var weaponsInfo = _content.Load<Dictionary<int, string>>("Data\\weapons");
            foreach (var i in weaponsInfo.Keys)
            {
                var item = Item.ParseWeaponFromString(weaponsInfo[i]);
                if (item.IsError())
                {
                    GemfruitMod.Logger.Log(LogLevel.Error, "ItemRegistry", item.UnwrapError().Message);
                }
                else
                {
                    var val = item.Unwrap();
                    val.AssignSpriteSheetReference(new ResourceKey("TileSheets\\weapons"), VanillaSpritesheetHelper.WeaponIdToRectangle(i));
                    val.Key = VanillaResourceKeyTransformers.ApplyTransformerForKey(val, i, new ResourceKey(StringUtility.SanitizeName(val.Name)));
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
                    $"Attempted to register '{key}' before/after corresponding lifecycle event!");
            }
        }

        public Optional<Item> Get(ResourceKey key)
        {
            return _dictionary.ContainsKey(key) ? new Optional<Item>(_dictionary[key]) : Optional<Item>.None();
        }
    }
}