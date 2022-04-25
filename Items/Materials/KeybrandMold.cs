using Terraria.ModLoader;
using Terraria.ID;

namespace KeybrandsPlus.Items.Materials
{
    class KeybrandMold : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used to forge basic keybrands");
        }
        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 18;
            item.rare = ItemRarityID.White;
            item.maxStack = 999;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddRecipeGroup("K+:T2Bar", 10);
            r.AddIngredient(ItemID.ClayBlock, 10);
            r.AddTile(TileID.Furnaces);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
