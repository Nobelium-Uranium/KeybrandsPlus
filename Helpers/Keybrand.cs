using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace KeybrandsPlus.Helpers
{
    public abstract class Keybrand : ModItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.maxStack = 1;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
        }
    }
}