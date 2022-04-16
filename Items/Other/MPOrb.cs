using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Other
{
    class MPOrb : ModItem
    {
        public override bool CloneNewInstances { get { return true; } }

        public int TimeLeft;
        public float Scale;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MP Orb");
            Tooltip.SetDefault("You aren't supposed to have this in your inventory");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Blue;
            item.Size = new Vector2(12);
            TimeLeft = 300;
            Scale = 1f;
            item.maxStack = 100;
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (TimeLeft > 0)
                TimeLeft--;
            else
            {
                if (Scale <= 0.1)
                    item.active = false;
                Scale -= 0.1f;
            }
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.White.ToVector3() * 0.1f * Scale);
        }
        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange *= 3;
        }
        public override bool GrabStyle(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                Vector2 vectorItemToPlayer = player.Center - item.Center;
                Vector2 movement = vectorItemToPlayer.SafeNormalize(default) * 5f;
                item.velocity += movement;
                AdjustMagnitude(ref item.velocity, 15f);
            }
            return true;
        }
        private void AdjustMagnitude(ref Vector2 vector, float max)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > max)
            {
                vector *= max / magnitude;
            }
        }
        public override bool ItemSpace(Player player)
        {
            return true;
        }
        public override bool OnPickup(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/MPOrb").WithVolume(0.8f), player.Center);
            if (!player.GetModPlayer<Globals.KeyPlayer>().rechargeMP)
            {
                if (Main.myPlayer == player.whoAmI)
                    CombatText.NewText(player.getRect(), Color.DodgerBlue, item.stack);
                player.GetModPlayer<Globals.KeyPlayer>().currentMP += item.stack;
            }
            else
            {
                if (Main.myPlayer == player.whoAmI)
                    CombatText.NewText(player.getRect(), Color.DodgerBlue, item.stack / 250f * 100 + "%");
                player.GetModPlayer<Globals.KeyPlayer>().rechargeMPTimer -= (int)(player.GetModPlayer<Globals.KeyPlayer>().rechargeMPTimer * (item.stack / 250f));
            }
            if (player.GetModPlayer<Globals.KeyPlayer>().currentMP > player.GetModPlayer<Globals.KeyPlayer>().maxMP)
                player.GetModPlayer<Globals.KeyPlayer>().currentMP = player.GetModPlayer<Globals.KeyPlayer>().maxMP;
            return false;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 origin = sourceRectangle.Size() / 2f;
            Color drawColor = item.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture, item.Center - Main.screenPosition, sourceRectangle, Color.White * .75f * Main.essScale, rotation, origin, Scale * Main.essScale, SpriteEffects.None, 0f);
            return false;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = Main.itemTexture[item.type];
            spriteBatch.Draw(texture, position, null, Color.White * .75f, 0, origin, scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
