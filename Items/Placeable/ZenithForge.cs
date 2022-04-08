using Terraria;
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
            item.value = Item.sellPrice(platinum: 1);
            item.GetGlobalItem<Globals.KeyRarity>().ZenithRarity = true;
        }

        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<Materials.AeroCarbonAlloy>(), 20);
            r.AddIngredient(ModContent.ItemType<Materials.AerosteelPlating>(), 15);
            r.AddIngredient(ModContent.ItemType<Materials.Aerogel>(), 5);
            r.AddIngredient(ModContent.ItemType<Synthesis.Other.ZenithitePlus>());
            r.AddTile(TileID.HeavyWorkBench);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
