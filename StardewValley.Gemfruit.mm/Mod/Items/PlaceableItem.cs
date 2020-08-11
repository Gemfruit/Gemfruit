using Gemfruit.Mod.Placeables;

namespace Gemfruit.Mod.Items
{
    public class PlaceableItem : Item
    {
        public override int StackSize => 1;

        public PlaceableItem(Placeable p)
        {
            Key = p.Key;
            Name = p.Name;
            Price = p.Price;
            Type = "Decor";
            Category = -24;
            DisplayName = p.DisplayName;
            Description = p.Description;
            AssignSpriteSheetReference(p.SpriteSheet, p.Rect);
        }
    }
}