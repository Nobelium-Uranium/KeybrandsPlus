using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;

namespace KeybrandsPlus.Buffs
{
    class CureCooldown : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Healing Exhaustion");
            Description.SetDefault("Cannot use Cure");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<KeyPlayer>().CureCooldown = true;
        }
    }
}
