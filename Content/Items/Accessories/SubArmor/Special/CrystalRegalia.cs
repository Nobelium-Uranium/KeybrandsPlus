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
            Tooltip.SetDefault("20% increased damage\n" +
                "Increases max life and mana by 200\n" +
                "Increases your max number of minions and sentries by 2");
        }
        public override void SafeSetDefaults()
        {
            Item.rare = ModContent.RarityType<EffervescentRarity>();
            Item.defense = 20;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += .2f;
            player.statLifeMax2 += 200;
            player.statManaMax2 += 200;
            player.maxMinions += 2;
            player.maxTurrets += 2;
        }
    }
}
