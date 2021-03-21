using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Other
{
    public class Junk : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Completely useless\n" +
                "Seriously, don't even try to find a use for it\n" +
                "'Next time use a keybrand'");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(20);
            item.rare = ItemRarityID.Gray;
        }
    }
}
