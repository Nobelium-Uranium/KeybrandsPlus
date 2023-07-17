using KeybrandsPlus.Common.Helpers;
using KeybrandsPlus.Content.Items.Currency;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.Globals
{
    public class KeyNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.value >= 100)
            {
                int munny = (int)Math.Floor(npc.value / 100);
                if (KeyUtils.ProbablyABoss(npc))
                    munny /= 2;
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Munny>(), 1, munny, munny));
            }
        }
    }
}
