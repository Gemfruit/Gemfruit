using System;
using Gemfruit.Mod;
using Gemfruit.Mod.API;
using Gemfruit.Mod.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;

#pragma warning disable 108,114,626,649
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace StardewValley.Menus
{
    internal class patch_TitleMenu : TitleMenu
    {
        private float scale = 1.0f;
        private bool rev;
        private const float margin = 0.25f;
        private const float increment = margin / 60.0f;
        protected extern void orig_draw(SpriteBatch b);

        private static Rectangle render;
        
        public override void draw(SpriteBatch b) {
            orig_draw(b);
            var text = $"Using GemFruit v{GemfruitMod.GemfruitVersion}";
            var x = Game1.smallFont.MeasureString(text).X * scale / 2f;
            
            if (scale >= 1.0f + margin)
            {
                rev = true;
                scale = 1.0f + margin;
            }
            else if (scale <= 1.0f)
            {
                rev = false;
                scale = 1.0f;
            }

            if (rev)
                scale -= increment;
            else
                scale += increment;
            Utility.drawTextWithColoredShadow(b, text
                , 
                Game1.smallFont, 
                new Vector2(width / 2.0f - x - 1, 17), 
                Color.Black,
                Color.Black,
                numShadows: 0,
                scale: scale);
            Utility.drawTextWithColoredShadow(b, 
                text, 
                Game1.smallFont, 
                new Vector2(width / 2.0f - x, 16), 
                Color.White,
                Color.Black,
                numShadows: 0,
                scale: scale);
        }
    }
}