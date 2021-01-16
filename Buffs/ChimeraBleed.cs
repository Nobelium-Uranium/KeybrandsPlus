using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Buffs
{
    class ChimeraBleed : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Chimera Fang");
            Description.SetDefault("Your blood refuses to stay in your body");
            Main.debuff[Type] = true;
            canBeCleared = false;
            longerExpertDebuff = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<KeyNPC>().ChimeraBleed = true;
            int Blood = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood);
            Main.dust[Blood].position -= new Vector2(4, 4);
            Main.dust[Blood].velocity = Vector2.Zero;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<KeyPlayer>().ChimeraBleed = true;
            int Blood = Dust.NewDust(player.position, player.width, player.height, DustID.Blood);
            Main.dust[Blood].position -= new Vector2(4, 4);
            Main.dust[Blood].velocity = Vector2.Zero;
        }
    }
}
