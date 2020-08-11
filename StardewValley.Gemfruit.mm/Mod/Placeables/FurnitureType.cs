using System;
using Microsoft.Xna.Framework;

namespace Gemfruit.Mod.Items
{
    public enum FurnitureType
    {
        Chair = 0,
        Bench = 1,
        Couch = 2,
        Armchair = 3,
        Dresser = 4,
        LongTable = 5,
        Painting = 6,
        Lamp = 7,
        Decor = 8,
        Other = 9,
        Bookcase = 10,
        Table = 11,
        Rug = 12,
        Window = 13,
        Fireplace = 14
    }

    public static class FurnitureTypeExt
    {

        public static FurnitureType FromString(string str)
        {
            switch (str)
            {
                case "chair":
                    return FurnitureType.Chair;
                case "bench":
                    return FurnitureType.Bench;
                case "couch":
                    return FurnitureType.Couch;
                case "armchair":
                    return FurnitureType.Armchair;
                case "dresser":
                    return FurnitureType.Dresser;
                case "long table":
                    return FurnitureType.LongTable;
                case "painting":
                    return FurnitureType.Painting;
                case "lamp":
                    return FurnitureType.Lamp;
                case "decor":
                    return FurnitureType.Decor;
                case "other":
                    return FurnitureType.Other;
                case "bookcase":
                    return FurnitureType.Bookcase;
                case "table":
                    return FurnitureType.Table;
                case "rug":
                    return FurnitureType.Rug;
                case "window":
                    return FurnitureType.Window;
                case "fireplace":
                    return FurnitureType.Fireplace;
                default:
                    throw new Exception($"couldn't parse FurnitureType '{str}'");
            }
        } 
        
        public static Point GetSizeForType(this FurnitureType type)
        {
            switch (type)
            {
                case FurnitureType.Chair:
                case FurnitureType.Decor:
                case FurnitureType.Window:
                    return new Point(16, 32);
                case FurnitureType.Armchair:
                case FurnitureType.Dresser:
                case FurnitureType.Bench:
                case FurnitureType.Painting:
                    return new Point(32, 32);
                case FurnitureType.Couch:
                case FurnitureType.Rug:
                    return new Point(48, 32);
                case FurnitureType.LongTable:
                    return new Point(80, 48);
                case FurnitureType.Lamp:
                    return new Point(16, 48);
                case FurnitureType.Bookcase:
                case FurnitureType.Table:
                    return new Point(32, 48);
                case FurnitureType.Fireplace:
                    return new Point(32, 80);
                default:
                    return new Point(16, 32);
            }
        }

        public static Point GetBoundsForType(this FurnitureType type)
        {
            switch (type)
            {
                case FurnitureType.Chair:
                case FurnitureType.Lamp:
                case FurnitureType.Decor:
                    return new Point(64,64);
                case FurnitureType.Bench:
                case FurnitureType.Painting:
                case FurnitureType.Table:
                    return new Point(128, 128);
                case FurnitureType.Couch:
                case FurnitureType.Rug:
                    return new Point(192, 128);
                case FurnitureType.Armchair:
                case FurnitureType.Dresser:
                case FurnitureType.Bookcase:
                case FurnitureType.Fireplace:
                    return new Point(128, 64);
                case FurnitureType.LongTable:
                    return new Point(320, 64);
                case FurnitureType.Window:
                    return new Point(64, 128);
                default:
                    return new Point(64, 64);
            }
        }
    }
}