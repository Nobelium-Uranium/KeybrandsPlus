using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;

namespace KeybrandsPlus.Buffs
{
    class SecondChanceCooldown : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Last Chance");
            Description.SetDefault("Lifesteal is disabled");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<KeyPlayer>().SCCooldown = true;
            player.buffImmune[ModContent.BuffType<SecondChance>()] = true;
        }
    }
}
