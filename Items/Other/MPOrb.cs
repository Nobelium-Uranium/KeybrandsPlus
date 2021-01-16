﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            DisplayName.SetDefault("MP Prize");
            Tooltip.SetDefault("If you somehow got this in your inventory, it's a bug\nPlease let the mod developer know about this\n...Unless you got it via HERO's Mod, Cheat Sheet or the such, you cheater");
        }

        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Blue;
            item.Size = new Vector2(12);
            TimeLeft = 300;
            Scale = 1f;
            item.maxStack = 100;
        }
        public override Color? GetAlpha(Color lightColor) => Color.White * .75f * Main.essScale;
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
            grabRange = (int)(grabRange * 1.5f);
        }
        public override bool GrabStyle(Player player)
        {
            Vector2 vectorItemToPlayer = player.Center - item.Center;
            Vector2 movement = vectorItemToPlayer.SafeNormalize(default) * 10f;
            item.velocity = movement;
            return true;
        }
        public override bool ItemSpace(Player player)
        {
            return true;
        }
        public override bool OnPickup(Player player)
        {
            Main.PlaySound(SoundID.Item30.WithVolume(0.1f), player.Center);
            player.statMana += item.stack;
            if (player.statMana > player.statManaMax2)
                player.statMana = player.statManaMax2;
            CombatText.NewText(player.getRect(), CombatText.HealMana, item.stack, dot: true);
            return false;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 origin = sourceRectangle.Size() / 2f;
            Color drawColor = item.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture, item.Center - Main.screenPosition, sourceRectangle, drawColor, rotation, origin, Scale * Main.essScale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
