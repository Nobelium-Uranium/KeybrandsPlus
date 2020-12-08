using Terraria.ModLoader;
using Terraria.ID;

namespace KeybrandsPlus.Items.Materials
{
    class UltimaBlueprint : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Filled with the schematics needed to forge legendary keybrands");
        }
        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 42;
            item.rare = ItemRarityID.Purple;
            item.maxStack = 1;
        }
    }
}
