using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using System;
using KeybrandsPlus.Helpers;

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
                KeyUtils.NewDustCircular(out int dust, item.Center - new Vector2(4, 12), Vector2.Zero, 111, Main.rand.NextFloat(.5f, 1.25f), perfect: true);
            }
            return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
    }
    class ZenithitePlus : ModItem
    {
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
            item.GetGlobalItem<Globals.KeyRarity>().ZenithRarity = true;
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
                KeyUtils.NewDustCircular(out int dust, item.Center - new Vector2(4, 12), Vector2.Zero, 111, Main.rand.NextFloat(1f, 1.75f), perfect: true);
            }
            return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange *= 2;
        }
    }
}
