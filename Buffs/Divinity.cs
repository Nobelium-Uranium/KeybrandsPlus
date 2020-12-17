using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;

namespace KeybrandsPlus.Buffs
{
    class Divinity : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Divine Magic");
            Description.SetDefault("Your magical abilities are improved\n" +
                "However, your strength wanes");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<KeyPlayer>().Divinity = true;
        }
    }
}
