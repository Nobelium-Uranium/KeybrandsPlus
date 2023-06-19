using System;
using System.Collections.Generic;
using System.Text;
using KeybrandsPlus.Common.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.UI
{
    public static class MunnyUI
    {
        private static Texture2D munnyTexture;

        internal static void Load()
        {
            munnyTexture = ModContent.Request<Texture2D>($"{nameof(KeybrandsPlus)}/Assets/Textures/MunnyPouch", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        }
        internal static void Unload()
        {
            munnyTexture = null;
        }
        public static void Draw(SpriteBatch spriteBatch, Player player)
        {
            KeyPlayer modPlayer = player.GetModPlayer<KeyPlayer>();
            float uiScale = Main.UIScale;
            Vector2 pos = new Vector2((8 + munnyTexture.Width / 2) * uiScale, Main.screenHeight * .375f);
            spriteBatch.Draw(munnyTexture, pos, null, Color.White, 0f, munnyTexture.Size() * .5f, uiScale, SpriteEffects.None, 0);
            string text = $"{modPlayer.MunnyCount}";
            pos.Y -= munnyTexture.Height * .125f * uiScale;
            pos.X += (4 + munnyTexture.Width / 2) * uiScale;
            Utils.DrawBorderString(spriteBatch, text, pos, Color.Goldenrod, 1.25f);
            if (modPlayer.recentMunnyCounter > 0 && modPlayer.recentMunny != 0)
            {
                string sign = "+";
                if (modPlayer.recentMunny < 0)
                    sign = "-";
                text = $"{sign}{Math.Abs(modPlayer.recentMunny)}";
                pos.Y -= munnyTexture.Height * .375f * uiScale;
                Utils.DrawBorderString(spriteBatch, text, pos, Color.Goldenrod);
            }
        }
    }

}
