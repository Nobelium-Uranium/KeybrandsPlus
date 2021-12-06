using KeybrandsPlus.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace KeybrandsPlus.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
    class BlossomWings : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternal Blossom Wings");
            Tooltip.SetDefault("20% increased elemental damage and resistance\n" +
                "Allows for very long lasting flight\n" +
                "Just don't get hit\n" +
                "'Plucked from an otherworldly plant'");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.rare = ItemRarityID.Cyan;
            item.accessory = true;
            item.GetGlobalItem<KeyRarity>().ProudRarity = true;
            item.GetGlobalItem<KeyItem>().IsSpecial = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 3600;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = .75f;
            ascentWhenRising = .15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 2.5f;
            constantAscend = .125f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 7f;
            acceleration *= 1.5f;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<KeyPlayer>().BlossomWings = true;
            player.GetModPlayer<KeyPlayer>().ChainResistFire += .2f;
            player.GetModPlayer<KeyPlayer>().ChainResistBlizzard += .2f;
            player.GetModPlayer<KeyPlayer>().ChainResistThunder += .2f;
            player.GetModPlayer<KeyPlayer>().ChainResistAero += .2f;
            player.GetModPlayer<KeyPlayer>().ChainResistWater += .2f;
            player.GetModPlayer<KeyPlayer>().ChainResistLight += .2f;
            player.GetModPlayer<KeyPlayer>().ChainResistDark += .2f;
        }
    }
}
