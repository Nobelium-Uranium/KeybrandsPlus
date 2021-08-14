using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;

namespace KeybrandsPlus.Buffs
{
    class MPRegeneration : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("MP Regeneration");
            Description.SetDefault("Potent magic flows through your lifestream");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<KeyPlayer>().regenMP = true;
        }
    }
}
