using KeybrandsPlus.Common.Globals;
using KeybrandsPlus.Common.Helpers;

namespace KeybrandsPlus.Content.Items.Accessories.SubArmor.Utility
{
    public class MasterTreasureMagnet : SubArmorItem
    {
        public override void SafeSetDefaults()
        {
            Item.rare = ItemRarityID.LightRed;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<KeyPlayer>().MunnyMagnet = true;
            player.treasureMagnet = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<MunnyMagnet>()
                .AddIngredient(ItemID.TreasureMagnet)
                .AddIngredient(ItemID.SoulofFright)
                .AddIngredient(ItemID.SoulofMight)
                .AddIngredient(ItemID.SoulofSight)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
