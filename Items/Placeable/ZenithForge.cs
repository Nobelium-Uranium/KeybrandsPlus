using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Placeable
{
    public class ZenithForge : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenitherium Crucible");
            Tooltip.SetDefault("'Its seemingly pitiful flame holds untapped potential...'");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = ModContent.TileType<Tiles.ZenithForge>();
            item.rare = ItemRarityID.Cyan;
            item.GetGlobalItem<Globals.KeyRarity>().ZenithRarity = true;
        }
    }
}
