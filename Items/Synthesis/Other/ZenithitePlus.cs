using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace KeybrandsPlus.Items.Synthesis.Other
{
    class ZenithitePlus : Helpers.GlowmaskItem
    {
        public override Color[] ItemNameCycleColors => new Color[] { new Color(0, 250, 190), Color.White, new Color(0, 250, 190), new Color(0, 125, 95) };
        public override int ItemNameCycleRate => 30;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenithite+");
            Tooltip.SetDefault("An extremely rare and valuable ore");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.rare = ItemRarityID.Purple;
            item.maxStack = 99;
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            item.velocity *= .96f;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.MediumSpringGreen.ToVector3() * 1f);
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            int center = Dust.NewDust(item.Center - new Vector2(4, 16), 0, 0, 111);
            Main.dust[center].velocity = Vector2.Zero;
            if (Math.Abs(item.velocity.Length()) > 1f)
                Main.dust[center].scale *= .75f;
            if (Main.rand.NextBool(3))
            {
                int dust = Dust.NewDust(item.Center - new Vector2(4, 16), 0, 0, 111);
                if (Main.rand.NextBool())
                    Main.dust[dust].velocity /= 2;
            }
            return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange *= 2;
        }
    }
}
