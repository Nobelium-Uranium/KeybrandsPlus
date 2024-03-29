﻿using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;

namespace KeybrandsPlus.Items.Materials
{
    class MysticFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystic Essence");
            Tooltip.SetDefault("A fragment of wonder and ruin\nFilled with inner strength");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(20);
            item.rare = ItemRarityID.Pink;
            item.maxStack = 999;
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.BlueViolet.ToVector3() * 0.35f * Main.essScale);
        }
    }
}
