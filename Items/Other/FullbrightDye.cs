using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Other
{
    class FullbrightDye : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultrabright Dye");
            Tooltip.SetDefault("'Because I couldn't be bothered'");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(20);
            item.maxStack = 99;
            item.value = Item.sellPrice(gold: 1, silver: 50);
            item.rare = ItemRarityID.Cyan;
            item.GetGlobalItem<KeyRarity>().DeveloperRarity = true;
            item.GetGlobalItem<KeyRarity>().DeveloperName = "ChemAtDark";
        }
    }
}
