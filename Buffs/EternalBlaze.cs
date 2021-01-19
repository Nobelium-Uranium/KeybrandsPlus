using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Buffs
{
    class EternalBlaze : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Eternal Blaze");
            Description.SetDefault("Saps health quickly");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<Globals.KeyNPC>().EternalBlaze = true;
        }

        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            npc.GetGlobalNPC<Globals.KeyNPC>().BlazeStack++;
            if (npc.GetGlobalNPC<Globals.KeyNPC>().BlazeStack > 10)
                npc.GetGlobalNPC<Globals.KeyNPC>().BlazeStack = 10;
            else if (npc.GetGlobalNPC<Globals.KeyNPC>().BlazeStack < 1)
                npc.GetGlobalNPC<Globals.KeyNPC>().BlazeStack = 1;
            return base.ReApply(npc, time, buffIndex);
        }
    }
}
