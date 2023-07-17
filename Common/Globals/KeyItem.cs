using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.Globals
{
    public class KeyItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            int index = -1;
            if (item.type == ItemID.Keybrand)
            {
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Name.StartsWith("Tooltip"))
                    {
                        index = i;
                    }
                }
                if (index != -1)
                {
                    tooltips.Insert(index + 1, new TooltipLine(Mod, "KeybrandsPlus:KeybrandReplica", "'A replica of a well-renowned keyblade'"));
                }
            }
        }
    }
}
