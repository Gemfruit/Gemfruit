using System;
using System.Collections.Generic;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Utility;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Items;
using Gemfruit.Mod.Placeables.Capabilities;
using Microsoft.Xna.Framework;

namespace Gemfruit.Mod.Placeables
{
    public class Placeable
    {
        public ResourceKey Key { get; set; }
        public ResourceKey SpriteSheet { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public Rectangle Rect { get; protected set; }
        public int Price { get; protected set; }
        
        // TODO: Overhaul this entire thing.
        public string DisplayName { get; protected set; }
        
        public Point Bounds { get; protected set; }
        public int Rotations { get; protected set; }
        
        public List<PlaceableCapability> Capabilities { get; protected set; }

        public Placeable()
        {
            Capabilities = new List<PlaceableCapability>();
        }
        
        internal static Result<Placeable, Exception> ParseFromBigCraftableString(string line)
        {
            var i = new Placeable();
            var parts = line.Split('/');

            try
            {
                // index 0 - Name
                i.Name = parts[0];
                
                // index 1 - Price
                i.Price = int.Parse(parts[1]);
                
                // index 2 - Edibility
                // We ignore this (for now) because there's not really any usage of it in the vanilla files.
                
                // index 3 - Type & Category
                // We also ignore this, as they're all set to "Crafting -9"
                
                // index 4 - Description
                i.Description = parts[4];
                
                // index 5 - SetOutdoors
                var so = BoolUtility.Parse(parts[5]);
                
                // index 6 - SetIndoors
                var si = BoolUtility.Parse(parts[6]);

                // index 7 - Fragility
                var frag = int.Parse(parts[7]);
                
                var il = false;

                if (parts.Length >= 10)
                {
                    il = BoolUtility.Parse(parts[8]);
                    i.DisplayName = parts[9];
                }
                else
                {
                    i.DisplayName = parts[8];
                }
                
                var cap = new CraftingPlaceableCapability(so, si, frag, il);
                i.Capabilities.Add(cap);
                
                i.Bounds = new Point(64, 64);
                i.Rect = new Rectangle(Point.Zero, new Point(16, 32));
            }
            catch (Exception e)
            {
                return Result<Placeable, Exception>.FromException(e);
            }

            return Result<Placeable, Exception>.FromValue(i);
        }
        
        internal static Result<Placeable, Exception> ParseFromFurnitureString(string line, string desc)
        {
            var i = new Placeable();
            var parts = line.Split('/');
            
            try
            {
                // index 0 - Name
                i.Name = parts[0];
                
                // index 1 - furniture type
                var f = FurnitureTypeExt.FromString(parts[1]);
                i.Capabilities.Add(new FurniturePlaceableCapability(f));

                // index 2 - src rect
                if (parts[2] == "-1")
                {
                    i.Rect = new Rectangle(Point.Zero, f.GetSizeForType());
                }
                else
                {
                    var comp = parts[2].Split(' ');
                    var size = new Point(int.Parse(comp[0])*16, int.Parse(comp[1])*16);
                    i.Rect = new Rectangle(Point.Zero, size);
                }

                // index 3 - bounding box
                if (parts[3] == "-1")
                {
                    i.Bounds = f.GetBoundsForType();
                }
                else
                {
                    var comp = parts[3].Split(' ');
                    i.Bounds = new Point(int.Parse(comp[0])*64, int.Parse(comp[1])*64);
                }

                // index 4 - rotations
                i.Rotations = int.Parse(parts[4]);

                // index 5 - price
                i.Price = int.Parse(parts[5]);

                // TODO: Better localization system.
                i.DisplayName = parts.Length > 6 ? parts[6] : i.Name;
                
                i.Description = desc;
            }
            catch (Exception e)
            {
                return Result<Placeable, Exception>.FromException(e);
            }

            return Result<Placeable, Exception>.FromValue(i);
        }
        
        public void AssignSpriteSheetReference(ResourceKey sheet, Rectangle pos)
        {
            SpriteSheet = sheet;
            Rect = pos;
        }

        public virtual Item GetPlaceableItem()
        {
            return new Item(this);
        }
    }
}