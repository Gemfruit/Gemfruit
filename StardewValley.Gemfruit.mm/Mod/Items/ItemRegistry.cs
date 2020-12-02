using System;
using System.Collections.Generic;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Events;
using Gemfruit.Mod.API.Events.Items;
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
    public class ItemRegistry : KeyedRegistry<Item>
    {
        private readonly LocalizedContentManager _content;
        private readonly Dictionary<ResourceKey, Item> _dictionary = new Dictionary<ResourceKey, Item>();

        private static readonly Dictionary<ResourceKey, Action<Item>> AttachmentActions =
            new Dictionary<ResourceKey, Action<Item>>();

        static ItemRegistry()
        {
            // VEGETABLES
            AttachmentActions.Add(new ResourceKey("amaranth"), Seedable(new ResourceKey("amaranth_seeds")));
            AttachmentActions.Add(new ResourceKey("artichoke"), Seedable(new ResourceKey("artichoke_seeds")));
            AttachmentActions.Add(new ResourceKey("beet"), Seedable(new ResourceKey("beet_seeds")));
            AttachmentActions.Add(new ResourceKey("bok_choy"), Seedable(new ResourceKey("bok_choy_seeds")));
            AttachmentActions.Add(new ResourceKey("cauliflower"), Seedable(new ResourceKey("cauliflower_seeds")));
            AttachmentActions.Add(new ResourceKey("corn"), Seedable(new ResourceKey("corn_seeds")));
            AttachmentActions.Add(new ResourceKey("eggplant"), Seedable(new ResourceKey("eggplant_seeds")));
            AttachmentActions.Add(new ResourceKey("garlic"), Seedable(new ResourceKey("garlic_seeds")));
            AttachmentActions.Add(new ResourceKey("green_bean"), Seedable(new ResourceKey("bean_starter")));
            AttachmentActions.Add(new ResourceKey("hops"), Seedable(new ResourceKey("hops_starter")));
            AttachmentActions.Add(new ResourceKey("kale"), Seedable(new ResourceKey("kale_seeds")));
            AttachmentActions.Add(new ResourceKey("parsnip"), Seedable(new ResourceKey("parsnip_seeds")));
            AttachmentActions.Add(new ResourceKey("potato"), Seedable(new ResourceKey("potato_seeds")));
            AttachmentActions.Add(new ResourceKey("pumpkin"), Seedable(new ResourceKey("pumpkin_seeds")));
            AttachmentActions.Add(new ResourceKey("radish"), Seedable(new ResourceKey("radish_seeds")));
            AttachmentActions.Add(new ResourceKey("red_cabbage"), Seedable(new ResourceKey("red_cabbage_seeds")));
            AttachmentActions.Add(new ResourceKey("tomato"), Seedable(new ResourceKey("tomato_seeds")));
            AttachmentActions.Add(new ResourceKey("unmilled_rice"), Seedable(new ResourceKey("rice_shoot")));
            AttachmentActions.Add(new ResourceKey("wheat"), Seedable(new ResourceKey("wheat_seeds")));
            AttachmentActions.Add(new ResourceKey("yam"), Seedable(new ResourceKey("yam_seeds")));

            // FRUIT
            AttachmentActions.Add(new ResourceKey("ancient_fruit"), Seedable(new ResourceKey("ancient_seeds")));
            AttachmentActions.Add(new ResourceKey("blueberry"), Seedable(new ResourceKey("blueberry_seeds")));
            AttachmentActions.Add(new ResourceKey("cactus_fruit"), Seedable(new ResourceKey("catcus_seeds")));
            AttachmentActions.Add(new ResourceKey("cranberries"), Seedable(new ResourceKey("cranberry_seeds")));
            AttachmentActions.Add(new ResourceKey("grape"), Seedable(new ResourceKey("grape_starter")));
            AttachmentActions.Add(new ResourceKey("hot_pepper"), Seedable(new ResourceKey("pepper_seeds")));
            AttachmentActions.Add(new ResourceKey("melon"), Seedable(new ResourceKey("melon_seeds")));
            AttachmentActions.Add(new ResourceKey("rhubarb"), Seedable(new ResourceKey("rhubarb_seeds")));
            AttachmentActions.Add(new ResourceKey("spice_berry"), Seedable(new ResourceKey("summer_seeds")));
            AttachmentActions.Add(new ResourceKey("starfruit"), Seedable(new ResourceKey("starfruit_seeds")));
            AttachmentActions.Add(new ResourceKey("strawberry"), Seedable(new ResourceKey("strawberry_seeds")));

            // OTHER SEEDS
            AttachmentActions.Add(new ResourceKey("wild_horseradish"),
                item => { item.AddCapability(new SeedableItemCapability(new ResourceKey("spring_seeds"))); });
            AttachmentActions.Add(new ResourceKey("common_mushroom"),
                item => { item.AddCapability(new SeedableItemCapability(new ResourceKey("fall_seeds"))); });
            AttachmentActions.Add(new ResourceKey("winter_root"),
                item => { item.AddCapability(new SeedableItemCapability(new ResourceKey("winter_seeds"))); });

            // SEEDS
            AttachmentActions.Add(new ResourceKey("rice_shoot"), Growable(new ResourceKey("unmilled_rice")));
            AttachmentActions.Add(new ResourceKey("amaranth_seeds"), Growable(new ResourceKey("amaranth")));
            AttachmentActions.Add(new ResourceKey("grape_starter"), Growable(new ResourceKey("grape")));
            AttachmentActions.Add(new ResourceKey("hops_starter"), Growable(new ResourceKey("hops")));
            AttachmentActions.Add(new ResourceKey("rare_seed"), Growable(new ResourceKey("gem_berry")));
            AttachmentActions.Add(new ResourceKey("fairy_seeds"), Growable(new ResourceKey("fairy_rose")));
            AttachmentActions.Add(new ResourceKey("tulip_bulb"), Growable(new ResourceKey("tulip")));
            AttachmentActions.Add(new ResourceKey("jazz_seeds"), Growable(new ResourceKey("blue_jazz")));
            AttachmentActions.Add(new ResourceKey("sunflower_seeds"), Growable(new ResourceKey("sunflower")));
            AttachmentActions.Add(new ResourceKey("coffee_bean"), Growable(new ResourceKey("coffee")));
            AttachmentActions.Add(new ResourceKey("poppy_seeds"), Growable(new ResourceKey("poppy")));
            AttachmentActions.Add(new ResourceKey("spangle_seeds"), Growable(new ResourceKey("summer_spangle")));
            AttachmentActions.Add(new ResourceKey("parsnip_seeds"), Growable(new ResourceKey("parsnip")));
            AttachmentActions.Add(new ResourceKey("bean_starter"), Growable(new ResourceKey("green_bean")));
            AttachmentActions.Add(new ResourceKey("cauliflower_seeds"), Growable(new ResourceKey("cauliflower")));
            AttachmentActions.Add(new ResourceKey("potato_seeds"), Growable(new ResourceKey("potato")));
            AttachmentActions.Add(new ResourceKey("garlic_seeds"), Growable(new ResourceKey("garlic")));
            AttachmentActions.Add(new ResourceKey("kale_seeds"), Growable(new ResourceKey("kale")));
            AttachmentActions.Add(new ResourceKey("rhubarb_seeds"), Growable(new ResourceKey("rhubarb")));
            AttachmentActions.Add(new ResourceKey("melon_seeds"), Growable(new ResourceKey("melon")));
            AttachmentActions.Add(new ResourceKey("tomato_seeds"), Growable(new ResourceKey("tomato")));
            AttachmentActions.Add(new ResourceKey("blueberry_seeds"), Growable(new ResourceKey("blueberry")));
            AttachmentActions.Add(new ResourceKey("pepper_seeds"), Growable(new ResourceKey("hot_pepper")));
            AttachmentActions.Add(new ResourceKey("wheat_seeds"), Growable(new ResourceKey("wheat")));
            AttachmentActions.Add(new ResourceKey("radish_seeds"), Growable(new ResourceKey("radish")));
            AttachmentActions.Add(new ResourceKey("red_cabbage_seeds"), Growable(new ResourceKey("red_cabbage")));
            AttachmentActions.Add(new ResourceKey("starfruit_seeds"), Growable(new ResourceKey("starfruit")));
            AttachmentActions.Add(new ResourceKey("corn_seeds"), Growable(new ResourceKey("corn")));
            AttachmentActions.Add(new ResourceKey("eggplant_seeds"), Growable(new ResourceKey("eggplant")));
            AttachmentActions.Add(new ResourceKey("artichoke_seeds"), Growable(new ResourceKey("artichoke")));
            AttachmentActions.Add(new ResourceKey("pumpkin_seeds"), Growable(new ResourceKey("pumpkin")));
            AttachmentActions.Add(new ResourceKey("bok_choy_seeds"), Growable(new ResourceKey("bok_choy")));
            AttachmentActions.Add(new ResourceKey("yam_seeds"), Growable(new ResourceKey("yam")));
            AttachmentActions.Add(new ResourceKey("cranberry_seeds"), Growable(new ResourceKey("cranberries")));
            AttachmentActions.Add(new ResourceKey("beet_seeds"), Growable(new ResourceKey("beet")));
            AttachmentActions.Add(new ResourceKey("spring_seeds"), Growable(new ResourceKey("spring_seeds")));
            AttachmentActions.Add(new ResourceKey("summer_seeds"), Growable(new ResourceKey("summer_seeds")));
            AttachmentActions.Add(new ResourceKey("fall_seeds"), Growable(new ResourceKey("fall_seeds")));
            AttachmentActions.Add(new ResourceKey("winter_seeds"), Growable(new ResourceKey("winter_seeds")));
            AttachmentActions.Add(new ResourceKey("ancient_seeds"), Growable(new ResourceKey("ancient_fruit")));
            AttachmentActions.Add(new ResourceKey("strawberry_seeds"), Growable(new ResourceKey("strawberry")));
            AttachmentActions.Add(new ResourceKey("cactus_seeds"), Growable(new ResourceKey("cactus_fruit")));
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
                        GemfruitMod.ArtifactDropRegistry.ParseVanillaItem(val.Key, objectInfo[i]);
                    }
                    
                    // If the item is a Geode, we have to add the extra data geodes require.
                    if (val.Name.Contains("Geode"))
                    {
                        GemfruitMod.GeodeResultRegistry.ParseVanillaItem(val.Key, objectInfo[i], objectInfo);
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

        protected override bool HasKey(ResourceKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        protected override void AddItem(ResourceKey key, Item item)
        {
            if (AttachmentActions.ContainsKey(key))
            {
                AttachmentActions[key].Invoke(item);
            }
            _dictionary.Add(key, item);
        }

        public Optional<Item> Get(ResourceKey key)
        {
            return _dictionary.ContainsKey(key) ? new Optional<Item>(_dictionary[key]) : Optional<Item>.None();
        }
    }
}