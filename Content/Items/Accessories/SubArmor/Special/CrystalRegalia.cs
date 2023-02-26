using KeybrandsPlus.Common.Globals;
using KeybrandsPlus.Common.Helpers;
using KeybrandsPlus.Common.Rarities;
using KeybrandsPlus.Content.Items.Currency;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Content.Items.Accessories.SubArmor.Special
{
    public class CrystalRegalia : SubArmorItem
    {
        public override void SafeSetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased damage\n" +
                "Increases max life and mana by 100\n" +
                "Increases your max number of minions and sentries by 1");
        }
        public override void SafeSetDefaults()
        {
            Item.rare = ModContent.RarityType<EffervescentRarity>();
            Item.defense = 10;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += .1f;
            player.statLifeMax2 += 100;
            player.statManaMax2 += 100;
            player.maxMinions++;
            player.maxTurrets++;
        }
    }
}
