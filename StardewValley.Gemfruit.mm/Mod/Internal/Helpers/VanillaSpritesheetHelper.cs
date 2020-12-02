using Microsoft.Xna.Framework;

namespace Gemfruit.Mod.Internal.Helpers
{
    /// <summary>
    /// Internal class for performing transformations on data relating to Spritesheets.
    /// </summary>
    /// <remarks>
    /// This static class largely serves to transform IDs and other forms of data into appreciable spritesheet
    /// data. This supplants the vanilla functionality which ties ID directly to position on a spritesheet -
    /// we use that calculation in a rough fashion to formalize a more mod-friendly approach to storing
    /// spritesheet data.
    /// </remarks>
    internal static class VanillaSpritesheetHelper
    {
        internal static Rectangle ItemIdToRectangle(int id)
        {
            var x = id % 24 * 16;
            var y = id / 24 * 16;
            return new Rectangle(x, y, 16, 16);
        }

        internal static Rectangle WeaponIdToRectangle(int id)
        {
            var x = id % 8 * 16;
            var y = id / 8 * 16;
            return new Rectangle(x, y, 16, 16);
        }
        
        internal static Point FurnitureIdToLocation(int id)
        {
            var x = id % 32 * 16;
            var y = id / 32 * 16;
            return new Point(x, y);
        }

        internal static Point CraftableIdToLocation(int id)
        {
            var x = id % 8 * 16;
            var y = id / 8 * 32;
            return new Point(x, y);
        }
    }
}