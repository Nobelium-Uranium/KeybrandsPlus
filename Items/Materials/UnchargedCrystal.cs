using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;

namespace KeybrandsPlus.Items.Materials
{
    class UnchargedCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Can be crafted into crystals that increase maximum MP");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(18, 28);
            item.rare = ItemRarityID.Blue;
            item.maxStack = 99;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return lightColor * Main.essScale;
        }
    }
}
