using System.Collections.Generic;
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
        public bool DeveloperRarity;
        public string DeveloperName = "Unknown";
        public bool ContributorRarity;
        public string ContributorName = "Unknown";
        public bool ProudRarity;
        public bool ZenithRarity;
        public bool ShadowRarity;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (DeveloperRarity)
            {
                for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
                {
                    TooltipLine line = tooltips[tooltip];
                    if (line.mod == "Terraria" && line.Name == "ItemName")
                    {
                        if (Midnight)
                            line.overrideColor = new Color(185, 0, 67);
                        else
                            line.overrideColor = Color.DodgerBlue;
                    }
                }
                tooltips.Add(new TooltipLine(mod, "DeveloperTooltip", "Developer: " + DeveloperName) { overrideColor = Color.DodgerBlue });
            }
            if (ContributorRarity)
            {
                for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
                {
                    TooltipLine line = tooltips[tooltip];
                    if (line.mod == "Terraria" && line.Name == "ItemName")
                    {
                        line.overrideColor = new Color(102, 255, 102);
                    }
                }
                tooltips.Add(new TooltipLine(mod, "ContributorTooltip", "Contributor: " + ContributorName) { overrideColor = new Color(102, 255, 102) });
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
                tooltips.Add(new TooltipLine(mod, "ProudTooltip", "Proud"));
            }
            else if (ZenithRarity)
            {
                for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
                {
                    TooltipLine line = tooltips[tooltip];
                    if (line.mod == "Terraria" && line.Name == "ItemName")
                    {
                        line.overrideColor = new Color(0, 250, 190);
                    }
                }
            }
            else if (ShadowRarity)
            {
                for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
                {
                    TooltipLine line = tooltips[tooltip];
                    if (line.mod == "Terraria" && line.Name == "ItemName")
                    {
                        line.overrideColor = new Color(25, 25, 25);
                    }
                }
            }
        }
    }
}
