using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;

namespace KeybrandsPlus.Buffs
{
    class EtherSickness : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Ether Sickness");
            Description.SetDefault("Cannot use Ethers");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<KeyPlayer>().EtherSickness = true;
        }
    }
}
