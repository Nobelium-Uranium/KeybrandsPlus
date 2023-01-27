using KeybrandsPlus.Common.Helpers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Content.Items.Accessories.SubArmor
{
    public class TestSubArmor : SubArmorItem
    {
        public override void SafeSetStaticDefaults()
        {
            DisplayName.SetDefault("Example Sub-Armor");
            Tooltip.SetDefault("10% increased damage");
        }
        public override void SafeSetDefaults()
        {
            Item.defense = 10;
            Item.rare = ItemRarityID.LightRed;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) *= 1.1f;
        }
    }
}
