using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Buffs
{
    class DragonRot : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Mirage Draconic Blaze");
            Description.SetDefault("Your flesh is dissolving... not really");
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<KeyNPC>().DragonRot = true;
            if (!Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<Dusts.DraconicFlame>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
                Main.dust[dust].velocity *= 0.5f;
            }
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (!Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, ModContent.DustType<Dusts.DraconicFlame>(), player.velocity.X * 0.4f, player.velocity.Y * 0.4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }
    }
}
