using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;
using KeybrandsPlus.Buffs;
using Terraria.Utilities;
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
                "Allows you to survive any fatal blow with 1 life remaining\n" +
                "This also grants extended invulnerability and cures most debuffs\n" +
                "Only triggers when above 1 life\n" +
                "This effect has a 10 second cooldown which is doubled in Expert mode");
        }
        public override void SetDefaults()
        {
            item.width = 15;
            item.height = 12;
            item.rare = ItemRarityID.Green;
            item.maxStack = 1;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<KeyPlayer>().LightAlignment += 20;
            player.GetModPlayer<KeyPlayer>().DarkAlignment -= 10;
            player.GetModPlayer<KeyPlayer>().CrownCharm = true;
            if (!player.GetModPlayer<KeyPlayer>().SCCooldown && player.statLife > 1)
            {
                player.AddBuff(ModContent.BuffType<SecondChance>(), 2);
            }
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            if (pre == -1)
                return false;
            return base.PrefixChance(pre, rand);
        }
    }
}
