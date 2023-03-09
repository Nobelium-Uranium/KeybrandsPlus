using KeybrandsPlus.Common.Globals;
using KeybrandsPlus.Common.Helpers;
using KeybrandsPlus.Common.Rarities;
using KeybrandsPlus.Content.Items.Currency;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Content.Items.Accessories.SubArmor.Utility
{
    public class GaleRing : SubArmorItem
    {
        public override void SafeSetStaticDefaults()
        {
            Tooltip.SetDefault("Replaces your grappling hook with an omnidirectional dash\n" +
                "Dash speed is dependant on your grappling hook's launch velocity\n" +
                "Up to 3 dashes can be performed in the air");
        }
        public override void SafeSetDefaults()
        {
            Item.rare = ModContent.RarityType<EffervescentRarity>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<KeyPlayer>().AirDash = true;
        }
    }
}
