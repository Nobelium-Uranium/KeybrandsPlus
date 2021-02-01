using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace KeybrandsPlus.Items.Synthesis.Other
{
    class Zenithite : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenithite");
            Tooltip.SetDefault("A very rare and valuable ore");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.rare = ItemRarityID.Cyan;
            item.maxStack = 999;
            item.GetGlobalItem<Globals.KeyRarity>().ZenithRarity = true;
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            item.velocity *= .96f;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.MediumSpringGreen.ToVector3() * .5f);
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            int center = Dust.NewDust(item.Center - new Vector2(4, 12), 0, 0, 111);
            Main.dust[center].velocity = Vector2.Zero;
            if (Math.Abs(item.velocity.Length()) > 1f)
                Main.dust[center].scale *= .75f;
            if (Main.rand.NextBool(7))
            {
                int dust = Dust.NewDust(item.Center - new Vector2(4, 12), 0, 0, 111);
                Main.dust[dust].velocity /= 2;
                if (Main.rand.NextBool())
                    Main.dust[dust].velocity /= 2;
            }
            return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
    }
}
