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
            Tooltip.SetDefault("+20 Light Alignment\n" +
                "-10 Dark Alignment\n" +
                "Taking a fatal hit will instead bring you down to 1HP\n" +
                "This also grants extended invulnerability and cures most debuffs\n" +
                "The effect only triggers when you aren't already at 1HP\n" +
                "15 second cooldown\n" +
                "Will not protect against instances of extreme blood loss");
        }
        public override void SetDefaults()
        {
            item.width = 15;
            item.height = 12;
            item.rare = ItemRarityID.Green;
            item.maxStack = 1;
            item.accessory = true;
            item.expert = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<KeyPlayer>().LightAlignment += 20;
            player.GetModPlayer<KeyPlayer>().DarkAlignment -= 10;
            player.GetModPlayer<KeyPlayer>().CrownCharm = true;
            if (!player.GetModPlayer<KeyPlayer>().SCCooldown && player.statLife > 1)
                player.AddBuff(ModContent.BuffType<SecondChance>(), 2);
        }
    }
}
