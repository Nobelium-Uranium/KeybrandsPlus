using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Other
{
    class WinnerStick : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("WINNER Stick");
            Tooltip.SetDefault("Can be sold for a high price");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(11);
            item.maxStack = 99;
            item.value = Item.sellPrice(gold: 20);
        }
    }
}
