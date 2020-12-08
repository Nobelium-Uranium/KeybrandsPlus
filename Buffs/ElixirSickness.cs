using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;

namespace KeybrandsPlus.Buffs
{
    class ElixirSickness : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Elixir Sickness");
            Description.SetDefault("Cannot use Elixirs");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<KeyPlayer>().ElixirSickness = true;
        }
    }
}
