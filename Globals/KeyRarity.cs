﻿using System.Collections.Generic;
using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Globals
{
    class KeyRarity : GlobalItem
    {
        public override bool InstancePerEntity { get { return true; } }
        public override bool CloneNewInstances { get { return true; } }
        
        public bool Midnight;
        public bool ContributorRarity;
        public string ContributorName = "Unknown";
        public bool ProudRarity;
        public bool ZenithRarity;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine Contributor = new TooltipLine(mod, "ContributorTooltip", "Contributor: " + ContributorName) { overrideColor = new Color(102, 255, 102) };
            TooltipLine Proud = new TooltipLine(mod, "ProudTooltip", "Proud");
            if (ContributorRarity)
            {
                for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
                {
                    TooltipLine line = tooltips[tooltip];
                    if (line.mod == "Terraria" && line.Name == "ItemName")
                    {
                        if (Midnight)
                            line.overrideColor = new Color(185, 0, 67);
                        else
                            line.overrideColor = new Color(102, 255, 102);
                    }
                }
                tooltips.Add(Contributor);
            }
            else if (ProudRarity)
            {
                for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
                {
                    TooltipLine line = tooltips[tooltip];
                    if (line.mod == "Terraria" && line.Name == "ItemName")
                    {
                        float fade = Main.GameUpdateCount % 60 / 60f;
                        line.overrideColor = Color.Lerp(Color.Red, Color.MediumVioletRed, fade);
                    }
                }
                tooltips.Add(Proud);
            }
            else if (ZenithRarity)
            {
                for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
                {
                    TooltipLine line = tooltips[tooltip];
                    if (line.mod == "Terraria" && line.Name == "ItemName")
                    {
                        line.overrideColor = new Color(0, 255, 189);
                    }
                }
            }
        }
    }
}
