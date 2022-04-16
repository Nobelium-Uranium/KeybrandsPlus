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
                "Extremely agile and allows for very long lasting flight\n" +
                "'Plucked from an otherworldly plant'");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.rare = ItemRarityID.Cyan;
            item.accessory = true;
            item.GetGlobalItem<KeyRarity>().ZenithRarity = true;
            item.GetGlobalItem<KeyRarity>().SecretRarity = true;
            item.GetGlobalItem<KeyItem>().IsSpecial = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 3600;
            if (!player.controlJump && !player.CCed)
            {
                if (player.controlDown)
                    player.maxFallSpeed *= 2.5f;
                else if (player.controlUp)
                {
                    player.fallStart = (int)(player.position.Y / 16f);
                    player.maxFallSpeed /= 2.5f;
                }
            }
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            if (player.controlDown)
            {
                maxAscentMultiplier = 1f;
                ascentWhenRising = 0f;
                maxCanAscendMultiplier = 1f;
                constantAscend = 0f;
                ascentWhenFalling = 1.25f;
            }
            else if (player.controlUp)
            {
                maxAscentMultiplier = 5f;
                ascentWhenRising = .45f;
                maxCanAscendMultiplier = 2.5f;
                constantAscend = .3f;
                ascentWhenFalling = 1.5f;
            }
            else
            {
                maxAscentMultiplier = 2.5f;
                ascentWhenRising = .15f;
                maxCanAscendMultiplier = 1f;
                constantAscend = .125f;
                ascentWhenFalling = .5f;
            }
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            if (player.controlJump && player.controlUp)
            {
                speed = 5f;
                acceleration *= 3.125f;
            }
            else if (player.controlJump && player.controlDown)
            {
                speed = 20f;
                acceleration *= 12.5f;
            }
            else
            {
                speed = 7.5f;
                acceleration *= 6.25f;
            }
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
