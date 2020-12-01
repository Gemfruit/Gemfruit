using System;
using System.Collections.Generic;
using System.Linq;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Utility;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Items.Capabilities;
using Gemfruit.Mod.Items.Container;
using Gemfruit.Mod.Placeables;
using Gemfruit.Mod.Placeables.Capabilities;
using Microsoft.Xna.Framework;

namespace Gemfruit.Mod.Items
{
    public class Item : IHasItemCapabilities, IHasContainers
    {
        public ResourceKey Key { get; set; }
        
        public ResourceKey SpriteSheet { get; protected set; }
        
        public Rectangle Rect { get; protected set; }
        
        public string Name { get; protected set; }
        public int Price { get; protected set; }
        public string Type { get; protected set; }
        public int Category { get; protected set; }
        public string DisplayName { get; protected set; }
        public string Description { get; protected set; }
        public int MaxStackSize { get; protected set; }

        private List<ItemCapability> Capabilities { get; }
        private List<IContainer> Containers { get; }

        public Item()
        {
            Capabilities = new List<ItemCapability>();
            Containers = new List<IContainer>();
        }
        

        public Item(Placeable p)
        {
            Key = p.Key;
            Name = p.Name;
            Price = p.Price;
            // TODO: This is probably an improper place to be doing this.
            if (p.Capabilities.Exists(m => m is FurniturePlaceableCapability))
            {
                Type = "Decor";
                Category = -24;
            }
            else if(p.Capabilities.Exists(m => m is CraftingPlaceableCapability))
            {
                Type = "Crafting";
                Category = -9;
            }
            else
            {
                Type = "INVALID";
                Category = -999;
            }
            DisplayName = p.DisplayName;
            Description = p.Description;
            MaxStackSize = 1;
            Capabilities = new List<ItemCapability> {new PlaceableItemCapability(p.Key)};
            AssignSpriteSheetReference(p.SpriteSheet, p.Rect);
        }
        
        internal static Result<Item, Exception> ParseWeaponFromString(string line)
        {
            var i = new Item();
            var parts = line.Split('/');

            try
            {
                // index 0 - Name
                i.Name = parts[0];

                // index 1 - description
                i.Description = parts[1];

                // index 2 - minimum damage
                var minimumDamage = int.Parse(parts[2]);

                // index 3 - maximum damage
                var maximumDamage = int.Parse(parts[3]);

                // index 4 - knockback
                var knockback = float.Parse(parts[4]);

                // index 5 - speed
                var speed = int.Parse(parts[5]);

                // index 6 - precision
                var precision = int.Parse(parts[6]);

                // index 7 - defense
                var defense = int.Parse(parts[7]);

                // index 8 - weapon type
                var weaponType = (WeaponType) int.Parse(parts[8]);

                // index 9 - base level
                var baseLevel = int.Parse(parts[9]);

                // index 10 - minimum level
                var minimumLevel = int.Parse(parts[10]);

                // index 11 - area of effect
                var areaOfEffect = int.Parse(parts[11]);

                // index 12 - crit chance
                var critChance = float.Parse(parts[12]);

                // index 13 - crit multiplier
                var critMultiplier = float.Parse(parts[13]);
                
                var weapondef = new WeaponizableItemCapability(minimumDamage, maximumDamage, knockback, 
                    speed, precision, defense, weaponType, baseLevel,
                    minimumLevel, areaOfEffect, critChance, critMultiplier);
                
                i.Capabilities.Add(weapondef);


                // TODO: Better localization system.
                i.DisplayName = parts.Length > 14 ? parts[14] : i.Name;
                
                // calculate this once, rather than continually
                i.Price = weapondef.ItemLevel * 100;
                
                // hardcoded defaults
                i.Type = "Weapon";
                i.Category = -98;
                i.MaxStackSize = 1;
            }
            catch (Exception e)
            {
                return Result<Item, Exception>.FromException(e);
            }

            return Result<Item, Exception>.FromValue(i);
        }

        internal static Result<Item, Exception> ParseFromString(string line)
        {
            var i = new Item();
            var parts = line.Split('/');
            try
            {
                // index 0 - Name
                i.Name = parts[0];
                // index 1 - Price
                i.Price = int.Parse(parts[1]);
                // index 2 - edibility
                var edible = int.Parse(parts[2]);
                // index 3 - type & category
                var tc = parts[3].Split(' ');
                i.Type = tc[0];
                if (tc.Length > 1) i.Category = int.Parse(tc[1]);
                // index 4 - display name
                i.DisplayName = parts[4];
                // index 5 - description
                i.Description = parts[5];

                if (parts.Length >= 9)
                {
                    switch (parts[6].ToLower())
                    {
                        case "food":
                            i.Capabilities.Add(new EdibleItemCapability(edible, FoodType.Food,
                                new FoodBuff(parts[7].Split().Select(int.Parse).ToList(), 
                                    int.Parse(parts[8]))));
                            break;
                        case "drink":
                            i.Capabilities.Add(new EdibleItemCapability(edible, FoodType.Drink,
                                new FoodBuff(parts[7].Split().Select(int.Parse).ToList(), 
                                    int.Parse(parts[8]))));
                            break;
                        default:
                            GemfruitMod.Logger.Log(LogLevel.Warning, "Item", "Item created from vanilla " +
                                                                             "string with edibility but no food type " +
                                                                             "specifier - ignoring edibility");
                            break;
                    }
                }

                // TODO: This is fairly fragile. Possibly change this?
                if (i.Name.Contains("Geode"))
                {
                    if (parts.Length > 7)
                    {
                        i.Capabilities.Add(new CrackableItemCapability(parts[6].Split().Select(int.Parse).ToList()));
                    }
                    else
                    {
                        GemfruitMod.Logger.Log(LogLevel.Warning, "Item", "Geode found but had no geode " +
                                                                         "information - it won't be treated like a " +
                                                                         "Geode!");
                    }
                }
            }
            catch (Exception e)
            {
                return Result<Item, Exception>.FromException(e);
            }

            return Result<Item, Exception>.FromValue(i);
        }

        public void AssignSpriteSheetReference(ResourceKey sheet, Rectangle pos)
        {
            SpriteSheet = sheet;
            Rect = pos;
        }

        public Result<TCap, Exception> GetCapability<TCap>() where TCap : ItemCapability
        {
            foreach (var c in Capabilities)
            {
                if (c is TCap cap)
                {
                    return Result<TCap, Exception>.FromValue(cap);
                }
            }
            return Result<TCap, Exception>.FromException(new Exception($"unavailable capability of type '{typeof(TCap).Name}'"));
        }

        public bool HasCapability<TCap>() where TCap : ItemCapability => Capabilities.Any(c => c is TCap);

        public bool HasCapability(Type capType)
        {
            return capType.IsSubclassOf(typeof(ItemCapability)) && Capabilities.Any(c => c.GetType() == capType);
        }

        public void AddCapability(ItemCapability capability)
        {
            if (HasCapability(capability.GetType()))
            {
                throw new Exception($"bad duplicate capability of type {capability.GetType().Name} in '{Name}'");
            }
            Capabilities.Add(capability);
            // TODO: Container initialization?
        }

        public Result<TCon, Exception> GetContainer<TCon>() where TCon : IContainer
        {
            foreach (var c in Containers)
            {
                if (c is TCon con)
                {
                    return Result<TCon, Exception>.FromValue(con);
                }
            }
            return Result<TCon, Exception>.FromException(new Exception($"unavailable container of type '{typeof(TCon).Name}'"));
        }

        public bool HasContainer<TCon>() where TCon : IContainer => Containers.Any(c => c is TCon);

        public bool HasContainer(Type containerType)
        {
            return containerType.IsSubclassOf(typeof(IContainer)) && Containers.Any(c => c.GetType() == containerType);
        }

        public bool TryFillContainer<TCon>(IReadOnlyDictionary<string, object> dict) where TCon : IContainer
        {
            // TODO: Implement
            throw new NotImplementedException();
        }
    }
}