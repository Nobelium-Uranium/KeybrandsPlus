using KeybrandsPlus.Common.Globals;
using KeybrandsPlus.Common.Helpers;
using KeybrandsPlus.Common.Rarities;

namespace KeybrandsPlus.Content.Items.Accessories.SubArmor.Special
{
    public class CrownCharm : SubArmorItem
    {
        public override void SafeSetDefaults()
        {
            Item.rare = ModContent.RarityType<ElectrumRarity>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.longInvince = true;
            if (player.statLife > (int)Math.Floor((float)player.statLifeMax2 * .05))
                player.GetModPlayer<KeyPlayer>().SecondChance = true;
        }
    }
}
