using KeybrandsPlus.Common.Globals;
using KeybrandsPlus.Common.Helpers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Content.Items.Accessories.SubArmor.Utility
{
    public class MunnyMagnet : SubArmorItem
    {
        public override void SafeSetStaticDefaults()
        {
            Tooltip.SetDefault("Increases pickup range for Munny\n" +
                "Landing critical hits sometimes drops a small amount of Munny");
        }
        public override void SafeSetDefaults()
        {
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<KeyPlayer>().MunnyMagnet = true;
        }
    }
}
