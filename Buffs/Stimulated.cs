using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;

namespace KeybrandsPlus.Buffs
{
    class Stimulated : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Stimulated Strength");
            Description.SetDefault("Your physical abilities are improved\n" +
                "However, your magic wanes");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<KeyPlayer>().Stimulated = true;
        }
    }
}
