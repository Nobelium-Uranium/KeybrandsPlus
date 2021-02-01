using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;

namespace KeybrandsPlus.Buffs
{
    class TurboExhaustion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Turbo Exhaustion");
            Description.SetDefault("Cannot use Turbo-Ethers");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<KeyPlayer>().TurboExhaustion = true;
        }
    }
}
