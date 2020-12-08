using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;

namespace KeybrandsPlus.Buffs
{
    class PanaceaCD : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Panacea Sickness");
            Description.SetDefault("Cannot use Panaceas");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<KeyPlayer>().PanaceaSickness = true;
        }
    }
}
