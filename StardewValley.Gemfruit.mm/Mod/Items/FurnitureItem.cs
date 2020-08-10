using System;
using Gemfruit.Mod.API.Utility;
using Microsoft.Xna.Framework;

namespace Gemfruit.Mod.Items
{
    public class FurnitureItem : Item
    {
        // TODO: Change this to handle localization.
        internal static string DefaultDescription = "";
        
        public FurnitureType FurnitureType { get; protected set; }

        public Point Bounds { get; protected set; }
        
        public int Rotations { get; protected set; }

        public override int StackSize => 1;

        internal static Result<FurnitureItem, Exception> ParseFromString(string line)
        {
            var i = new FurnitureItem();
            var parts = line.Split('/');

            try
            {
                // index 0 - Name
                i.Name = parts[0];

                // index 1 - furniture type
                i.FurnitureType = FurnitureTypeExt.FromString(parts[1]);

                // index 2 - src rect
                if (parts[2] == "-1")
                {
                    i.Rect = new Rectangle(Point.Zero, i.FurnitureType.GetSizeForType());
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
                    i.Bounds = i.FurnitureType.GetBoundsForType();
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

                // hardcoded defaults
                i.Edibility = 0;
                i.Type = "Decor";
                i.Category = -24;
                
                i.Description = DefaultDescription;
            }
            catch (Exception e)
            {
                return Result<FurnitureItem, Exception>.FromException(e);
            }

            return Result<FurnitureItem, Exception>.FromValue(i);
        }
    }
}