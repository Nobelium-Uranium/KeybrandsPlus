using Terraria.ModLoader;
using Terraria.ID;

namespace KeybrandsPlus.Items.Materials
{
    class RustedKeybrand : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Years of wear in the dungeon have made it dull\nPerhaps with some enchantment it can be repaired");
        }
        public override void SetDefaults()
        {
            item.width = 23;
            item.height = 25;
            item.rare = ItemRarityID.Gray;
            item.maxStack = 1;
        }
    }
}
