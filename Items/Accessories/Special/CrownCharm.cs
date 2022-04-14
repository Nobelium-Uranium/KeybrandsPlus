using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;
using KeybrandsPlus.Buffs;
using Terraria.ID;

namespace KeybrandsPlus.Items.Accessories.Special
{
    class CrownCharm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crown Charm");
            Tooltip.SetDefault("When equipped:\n" +
                "+20 Light Alignment\n" +
                "-10 Dark Alignment\n" +
                "10% increased Nil resistance\n" +
                "5% decreased damage with keybrands\n" +
                "While you are above 1HP, taking a fatal hit will instead bring you down to 1HP\n" +
                "This also cures most debuffs and has a brief cooldown");
        }
        public override void SetDefaults()
        {
            item.width = 15;
            item.height = 12;
            item.rare = ItemRarityID.Green;
            item.maxStack = 1;
            item.accessory = true;
            //item.expert = true;
            item.GetGlobalItem<KeyItem>().IsSpecial = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<KeyPlayer>().ChainResistNil += .1f;
            player.GetModPlayer<KeyPlayer>().RingAttackPhysical -= .05f;
            player.GetModPlayer<KeyPlayer>().RingAttackMagic -= .05f;
            player.GetModPlayer<KeyPlayer>().LightAlignment += 20;
            player.GetModPlayer<KeyPlayer>().DarkAlignment -= 10;
            player.GetModPlayer<KeyPlayer>().CrownCharm = true;
            if (!player.GetModPlayer<KeyPlayer>().SCCooldown && (player.GetModPlayer<KeyPlayer>().HollowSigil || player.statLife > 1))
                player.AddBuff(ModContent.BuffType<SecondChance>(), 2);
        }
    }
}
