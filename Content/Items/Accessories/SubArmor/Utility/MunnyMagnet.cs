using KeybrandsPlus.Common.Globals;
using KeybrandsPlus.Common.Helpers;
using Terraria;
using Terraria.ID;

namespace KeybrandsPlus.Content.Items.Accessories.SubArmor.Utility
{
    public class MunnyMagnet : SubArmorItem
    {
        public override void SafeSetStaticDefaults()
        {
            // Tooltip.SetDefault("Increases pickup range for Munny");
        }
        public override void SafeSetDefaults()
        {
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<KeyPlayer>().MunnyMagnet = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ShadowScale, 15)
                .AddTile(TileID.Anvils)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.TissueSample, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
