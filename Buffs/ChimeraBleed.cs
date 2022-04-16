using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<KeyNPC>().ChimeraBleed = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.buffTime[buffIndex] > (Main.expertMode ? 900 : 450))
                player.buffTime[buffIndex] = Main.expertMode ? 900 : 450;
            player.GetModPlayer<KeyPlayer>().ChimeraBleed = true;
            int Blood = Dust.NewDust(player.position, player.width, player.height, DustID.Blood);
            Main.dust[Blood].position -= new Vector2(4, 4);
            Main.dust[Blood].velocity = Vector2.Zero;
        }
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] += time > (Main.expertMode ? 600 : 300) ? (Main.expertMode ? 600 : 300) : time;
            if (player.buffTime[buffIndex] > (Main.expertMode ? 900 : 450))
                player.buffTime[buffIndex] = Main.expertMode ? 900 : 450;
            return false;
        }
    }
    class ChimeraMaul : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Chimera Maul");
            Description.SetDefault("Maximum life is reduced");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            string DeathText;
            if (Main.rand.NextBool(100))
                DeathText = player.name + " withered away.";
            else switch (Main.rand.Next(3))
                {
                    case 1:
                        DeathText = player.name + " was completely drained of blood.";
                        break;
                    case 2:
                        DeathText = player.name + "'s body became a lifeless husk.";
                        break;
                    default:
                        DeathText = player.name + " couldn't find the IV bag.";
                        break;
                }
            if (player.buffTime[buffIndex] >= 3600)
                player.KillMe(PlayerDeathReason.ByCustomReason(DeathText), 0, 0);
            int Blood = Dust.NewDust(player.position, player.width, player.height, DustID.Blood);
            Main.dust[Blood].position -= new Vector2(4, 4);
            Main.dust[Blood].velocity = Vector2.Zero;
            player.statLifeMax2 -= player.buffTime[buffIndex] / 3600 * player.statLifeMax2;
        }
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            string DeathText;
            if (Main.rand.NextBool(100))
                DeathText = player.name + " withered away.";
            else switch (Main.rand.Next(3))
                {
                    case 1:
                        DeathText = player.name + " was completely drained of blood.";
                        break;
                    case 2:
                        DeathText = player.name + "'s body became a lifeless husk.";
                        break;
                    default:
                        DeathText = player.name + " couldn't find the IV bag.";
                        break;
                }
            player.buffTime[buffIndex] += time;
            if (player.buffTime[buffIndex] > 3600)
                player.KillMe(PlayerDeathReason.ByCustomReason(DeathText), 0, 0);
            return false;
        }
    }
}
