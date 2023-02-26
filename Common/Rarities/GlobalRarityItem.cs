using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.Rarities
{
    public class GlobalRarityItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (KeybrandsPlus.keyRarities.TryGetValue(item.rare, out string name))
            {
                for (int i = 0; i < tooltips.Count; i++)
                {
                    TooltipLine line = tooltips[i];
                    if (line.Mod == "Terraria" && line.Name == "ItemName")
                    {
                        line.OverrideColor = ModContent.Find<ModRarity>(name).RarityColor;
                    }
                }
            }
        }
    }
}
