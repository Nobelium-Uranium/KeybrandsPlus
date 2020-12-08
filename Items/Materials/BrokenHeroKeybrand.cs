using Terraria.ModLoader;
using Terraria.ID;

namespace KeybrandsPlus.Items.Materials
{
    class BrokenHeroKeybrand : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Once a wielder's trusty friend, it's now rusted and damaged from neglect\nPerhaps it can be restored back to its former splendor...");
        }
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 42;
            item.rare = ItemRarityID.Yellow;
            item.maxStack = 99;
        }
    }
}
