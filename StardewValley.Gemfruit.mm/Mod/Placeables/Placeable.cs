using System;
using System.Collections.Generic;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Utility;
using Gemfruit.Mod.Items;
using Microsoft.Xna.Framework;

namespace Gemfruit.Mod.Placeables
{
    public class Placeable
    {
        public RegistryKey Key { get; }
        public RegistryKey SpriteSheet { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public Rectangle Rect { get; protected set; }
        public int Price { get; protected set; } 
        public string DisplayName { get; protected set; }
        public Point Bounds { get; protected set; }
        public int Rotations { get; protected set; }
        
        public List<PlaceableCapability> Capabilities { get; protected set; }

        
        internal static Result<Placeable, Exception> ParseFromFurnitureString(string line, string desc)
        {
            var i = new Placeable();
            var parts = line.Split('/');

            i.Capabilities = new List<PlaceableCapability>();
            
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
        
        public void AssignSpriteSheetReference(RegistryKey sheet, Rectangle pos)
        {
            SpriteSheet = sheet;
            Rect = pos;
        }

        public virtual PlaceableItem GetPlaceableItem()
        {
            return new PlaceableItem(this);
        }
    }
}