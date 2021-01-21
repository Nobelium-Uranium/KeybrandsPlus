using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace KeybrandsPlus.Items.Synthesis.Other
{
    class Zenithite : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenithite");
            Tooltip.SetDefault("A very rare and valuable ore");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.rare = ItemRarityID.Cyan;
            item.maxStack = 999;
            item.GetGlobalItem<Globals.KeyRarity>().ZenithRarity = true;
        }
    }
}
