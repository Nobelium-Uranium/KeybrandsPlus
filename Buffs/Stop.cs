using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Buffs
{
    class Stop : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Time Rupture");
            Description.SetDefault("See how powerless you are!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<Globals.KeyPlayer>().Stop = true;
        }
    }
}
