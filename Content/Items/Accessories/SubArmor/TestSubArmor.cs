using KeybrandsPlus.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Content.Items.Accessories.SubArmor
{
    public class TestSubArmor : SubArmorItem
    {
        public override void SafeSetStaticDefaults()
        {
            DisplayName.SetDefault("Example Sub-Armor");
            Tooltip.SetDefault("10% increased damage");
        }
        public override void SafeSetDefaults()
        {
            Item.defense = 10;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) *= 1.1f;
        }
    }
}
