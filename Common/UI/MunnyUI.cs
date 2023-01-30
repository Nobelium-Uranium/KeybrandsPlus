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
            munnyTexture = ModContent.Request<Texture2D>($"{nameof(KeybrandsPlus)}/Content/Items/Currency/MunnyPouch", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        }
        internal static void Unload()
        {
            munnyTexture = null;
        }
        public static void Draw(SpriteBatch spriteBatch, Player player)
        {
            KeyPlayer modPlayer = player.GetModPlayer<KeyPlayer>();
            float uiScale = Main.UIScale;
            Vector2 pos = new Vector2((4 + munnyTexture.Width / 2) * uiScale, Main.screenHeight * .375f);
            spriteBatch.Draw(munnyTexture, pos, null, Color.White, 0f, munnyTexture.Size() * .5f, uiScale, SpriteEffects.None, 0);
            string text = $"{modPlayer.CountMunny()}";
            pos.X += (4 + munnyTexture.Width / 2) * uiScale;
            Utils.DrawBorderString(spriteBatch, text, pos, Color.Goldenrod);
            if (modPlayer.recentMunnyCounter > 0)
            {
                pos.Y -= munnyTexture.Height * .25f * uiScale;
                text = $"+{modPlayer.recentMunny}";
                Utils.DrawBorderString(spriteBatch, text, pos, Color.Goldenrod, .75f);
            }
        }
    }

}
