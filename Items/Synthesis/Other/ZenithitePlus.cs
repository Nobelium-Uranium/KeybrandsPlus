﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace KeybrandsPlus.Items.Synthesis.Other
{
    class ZenithitePlus : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenithite+");
            Tooltip.SetDefault("An extremely rare and valuable ore");
        }
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;
            item.rare = ItemRarityID.Purple;
            item.maxStack = 99;
            item.GetGlobalItem<Globals.KeyRarity>().ZenithRarity = true;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.MediumSpringGreen.ToVector3() * 1f);
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            Dust.NewDust(item.position, 50, 50, 111);
        }
        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange *= 2;
        }
    }
}
