using KeybrandsPlus.Common.Globals;
using KeybrandsPlus.Common.Helpers;
using KeybrandsPlus.Common.Rarities;
using KeybrandsPlus.Content.Items.Currency;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Content.Items.Accessories.SubArmor.Special
{
    public class Extreme : SubArmorItem
    {
        public override void SafeSetStaticDefaults()
        {
            Tooltip.SetDefault("Sets defense to 0\n" +
                "Doubles damage dealt and taken");
        }
        public override void SafeSetDefaults()
        {
            Item.rare = ModContent.RarityType<EffervescentRarity>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<KeyPlayer>().Extreme = true;
            player.GetDamage(DamageClass.Generic) *= 2f;
        }
    }
}
