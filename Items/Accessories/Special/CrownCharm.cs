using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;
using KeybrandsPlus.Buffs;
using Terraria.Utilities;

namespace KeybrandsPlus.Items.Accessories.Special
{
    class CrownCharm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crown Charm");
            Tooltip.SetDefault("+20 Light Alignment\n-10 Dark Alignment\nAllows you to survive any fatal blow with 1 life remaining\nThis also grants extended invulnerability and cures most debuffs\nOnly triggers when above 1 life\n10 second cooldown");
        }
        public override void SetDefaults()
        {
            item.width = 15;
            item.height = 12;
            item.rare = 10;
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
