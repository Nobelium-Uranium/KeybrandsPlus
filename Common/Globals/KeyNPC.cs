using Terraria.ID;
using KeybrandsPlus.Content.Items.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (npc.boss || NPCID.Sets.ShouldBeCountedAsBoss[npc.type] || NPCID.Sets.BossHeadTextures[npc.type] != -1)
                    munny /= 2;
                int maxMunny = (int)Math.Ceiling(munny * 1.125f);
                int minMunny = Utils.Clamp((int)Math.Floor(munny * .875f), 1, maxMunny);
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Munny>(), 1, minMunny, maxMunny));
            }
        }
    }
}
